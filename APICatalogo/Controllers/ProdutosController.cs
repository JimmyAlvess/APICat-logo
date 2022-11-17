using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                return await _context.Produtos.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
            
        }
        [HttpGet("{id}",Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetId(int id)
        {
            try
            {
                var produto = await _context.Produtos.AsNoTracking()
              .FirstOrDefaultAsync(p => p.ProdutoId == id);
                if (produto == null)
                {
                    return NotFound($"Não foi possível encontrar o produtoId = {id}");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
          
        }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
            
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody]Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest($"Não foi possível atualizar o produto de id {id}");
                }

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
           
        }
        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                //var produto = _context.Produtos.Find(id);

                if (produto == null)
                {
                    return NotFound($"Não foi possível encontrar o produto de id {id}");
                }

                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentar obter as categoiras do banco de dados");
            }
          
        }
    }
}
