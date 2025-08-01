using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
    {
        return await _context.Categorias.ToListAsync();
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaID == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Categoria categoria)
    {
        if (categoria is null)
            return BadRequest();

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaID }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaID)
        {
            return BadRequest();
        }
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaID == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }
}