using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Produto.Application.DTOs;
using Produto.Application.Interfaces;
using Produto.Domain;

namespace ProdutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoCore>>> Get()
        {
            var produtos = await _service.ObterTodos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoCore>> GetById(int id)
        {
            var produto = await _service.ObterPorId(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produtoCriado = await _service.Criar(dto);
            return CreatedAtAction(nameof(GetById), new { id = produtoCriado.ID }, produtoCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProdutoDto dto)
        {
            var existente = await _service.ObterPorId(id);
            if (existente == null)
                return NotFound();

            await _service.Atualizar(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _service.ObterPorId(id);
            if (existente == null)
                return NotFound();

            await _service.Remover(id);
            return NoContent();
        }
    }
}
