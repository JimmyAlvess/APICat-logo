using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork contexto, IMapper mapper)
        {
            _context = contexto;
            _mapper = mapper;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProduto()
        {
            var categorias = _context.CategoriaRepository.GetCategoriasProdutos().ToList();
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get() 
        {
            try
            {
                var categorias = _context.CategoriaRepository.Get().AsNoTracking().ToList();
                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
                return categoriasDto;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            try
            {
                var categorias = _context.CategoriaRepository.GetById(c => c.CategoriaId == id);

                if (categorias == null)
                {
                    return NotFound($"A categoria com id={id} não foi encontrada");
                }

                var categoriasDto = _mapper.Map<CategoriaDTO>(categorias);
                return categoriasDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _context.CategoriaRepository.Add(categoria);
                _context.Commit();

                categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
                return new CreatedAtRouteResult("ObterProduto",
                       new { id = categoria.CategoriaId }, categoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
           
        }
        [HttpPut("{id}")]
        public ActionResult<Categoria> Put(int id,[FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                if (id != categoriaDto.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com id= {id}");
                }

                var categoria = _mapper.Map<Categoria>(categoriaDto);

                _context.CategoriaRepository.Update(categoria);
                _context.Commit();
                return Ok($"Categoria com id{id} foi atualizada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
          
        }
        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            try
            {
                var categoria = _context.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com id {id} não foi encontrada");
                }
                _context.CategoriaRepository.Delete(categoria);
                _context.Commit();

                var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);


                return categoriaDto;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
           
        }
    }
}
