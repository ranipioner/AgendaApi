using AgendaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto A", Preco = 10.00M, Quantidade = 100 },
            new Produto { Id = 2, Nome = "Produto B", Preco = 20.00M, Quantidade = 200 }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produto = produtos.FirstOrDefault(x => x.Id == id);
            if(produto == null)
            {
                return BadRequest();
            }
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto novoProduto)
        {
            if(novoProduto == null)
            {
                return BadRequest("Produto nulo");
            }
            novoProduto.Id = produtos.Max(p => p.Id) + 1;
            produtos.Add(novoProduto);

            return CreatedAtAction(nameof (Get), new {id = novoProduto.Id}, novoProduto);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Produto produtoAtualizado)
        {
            if(produtoAtualizado == null || produtoAtualizado.Id != id)
            {
                return BadRequest();
            }
            var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente == null)
            {
                return NotFound();
            }

            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;
            produtoExistente.Quantidade = produtoAtualizado.Quantidade;

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ; var produto = produtos.FirstOrDefault(p => p.Id == id);
            
            if(produto == null)
            {
                return NotFound();
            }
            produtos.Remove(produto);
            return NoContent();
        }

    }
}
