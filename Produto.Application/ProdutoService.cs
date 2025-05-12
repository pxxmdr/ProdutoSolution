using System.Collections.Generic;
using System.Threading.Tasks;
using Produto.Application.DTOs;
using Produto.Application.Interfaces;
using Produto.Domain;
using Produto.Domain.Interfaces;

namespace Produto.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;

        public ProdutoService(IProdutoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProdutoCore>> ObterTodos()
            => await _repo.ObterTodos();

        public async Task<ProdutoCore?> ObterPorId(int id)
            => await _repo.ObterPorId(id);

        public async Task<ProdutoCore> Criar(ProdutoDto dto)
        {
            var produto = new ProdutoCore
            {
                NOME = dto.Nome,
                DESCRICAO = dto.Descricao,
                VALOR = dto.Valor
            };
            await _repo.Adicionar(produto);
            return produto;
        }

        public async Task Atualizar(int id, ProdutoDto dto)
        {
            var produto = await _repo.ObterPorId(id);
            if (produto is null) return;

            produto.NOME = dto.Nome;
            produto.DESCRICAO = dto.Descricao;
            produto.VALOR = dto.Valor;
            await _repo.Atualizar(produto);
        }

        public async Task Remover(int id)
            => await _repo.Remover(id);
    }
}
