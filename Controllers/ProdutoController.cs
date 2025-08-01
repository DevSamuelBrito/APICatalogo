using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        //GET

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos.ToListAsync();

            if (produtos == null || !produtos.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoID == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }

        //POST

        [HttpPost]
        public async Task<ActionResult> Post(Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto inválido.");
            }
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = produto.ProdutoID }, produto);
        }

        //PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if (id != produto.ProdutoID)
            {
                return BadRequest("IDs não correspondem.");
            }

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Produto atualizado com sucesso.");

        }

        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return Ok("Produto removido com sucesso.");
        }

    }
}