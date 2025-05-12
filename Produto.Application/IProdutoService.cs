using Produto.Application.DTOs;
using Produto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produto.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoCore>> ObterTodos();
        Task<ProdutoCore?> ObterPorId(int id);
        Task<ProdutoCore> Criar(ProdutoDto dto);
        Task Atualizar(int id, ProdutoDto dto);
        Task Remover(int id);
    }
}
