using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Produto> GetProdutos()
        {
            return _context.Produtos;
        }
        public Produto GetProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == id);
            if(produto == null)
            {
                throw new KeyNotFoundException($"Produto with ID {id} not found.");
            }
            return produto;
        }

        public Produto Create(Produto produto)
        {
            if(produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "Produto cannot be null.");
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public bool Update(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "Produto cannot be null.");
            }

            if (_context.Produtos.Any(p => p.ProdutoID == produto.ProdutoID))
            {
                _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto is not null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}