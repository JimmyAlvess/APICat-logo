using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork contexto,IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {
            var produtos =  _uof.ProdutoRepository.GetProdutosPreco().ToList();
            var produtoDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtoDto;    
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            try
            {
                var produtos = _uof.ProdutoRepository.Get().ToList();
                var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
                return produtosDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
            
        }
        [HttpGet("{id}",Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> GetId(int id)
        {
            try
            {
                var produto =  _uof.ProdutoRepository.GetById(p => p.ProdutoId== id);
              
                if (produto == null)
                {
                    return NotFound($"Não foi possível encontrar o produtoId = {id}");
                }
                var produtoDto = _mapper.Map<ProdutoDTO>(produto);
                return produtoDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
          
        }

        [HttpPost]
        public ActionResult Post([FromBody]ProdutoDTO produtoDto)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoDto);
                _uof.ProdutoRepository.Add(produto);
                _uof.Commit();

                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produtoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
            
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody]ProdutoDTO produtoDto)
        {
            try
            {
                if (id != produtoDto.PrudutoId)
                {
                    return BadRequest($"Não foi possível atualizar o produto de id {id}");
                }

                var produto = _mapper.Map<Produto>(produtoDto);
                _uof.ProdutoRepository.Update(produto);
                _uof.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
           
        }
        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
                //var produto = _uof.Produtos.Find(id);

                if (produto == null)
                {
                    return NotFound($"Não foi possível encontrar o produto de id {id}");
                }

                _uof.ProdutoRepository.Delete(produto);
                _uof.Commit();

                var produtoDto = _mapper.Map<ProdutoDTO>(produto);
                return produtoDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
          
        }
    }
}
