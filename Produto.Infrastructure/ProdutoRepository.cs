using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Produto.Domain;
using Produto.Domain.Interfaces;
using Produto.Infrastructure.Data;

namespace Produto.Infrastructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext _context;

        public ProdutoRepository(ProdutoDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(ProdutoCore produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProdutoCore>> ObterTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<ProdutoCore?> ObterPorId(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task Atualizar(ProdutoCore produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
