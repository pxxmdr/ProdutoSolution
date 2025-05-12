using Produto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produto.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoCore>> ObterTodos();
        Task<ProdutoCore?> ObterPorId(int id);
        Task Adicionar(ProdutoCore produto);
        Task Atualizar(ProdutoCore produto);
        Task Remover(int id);
    }
}
