using AgendaApi.Models;
using AgendaApi.Sevices;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("api/v1[controller]")]
    public class PessoaController : Controller
    {
        private readonly PessoaService _service;

        public PessoaController(PessoaService service)
        {
            _service = service;
        }

        private static List<Pessoa> pessoas = new List<Pessoa>
        {
            new Pessoa {Id = 1, Nome = "Ranieri",Email = "ranieri@gmail.com" },
            new Pessoa {Id = 2, Nome = "Joao",Email = "joao@gmail.com" },

        };

        [HttpPost]
        public IActionResult Post([FromBody] Pessoa novaPessoa)
        {
            try
            {
                ValidaDados(novaPessoa);
                return Ok(_service.Save(novaPessoa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                return Ok(_service.GetALl());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_service.Get(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pessoa pessoaAtualizada)
        {

            try
            {
                ValidaDados(pessoaAtualizada);
                return Ok(_service.Update(pessoaAtualizada));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("Pessoa deletada com sucesso");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void ValidaDados(Pessoa pessoa)
        {
            if(pessoa.Nome == null || pessoa.Nome == "")
            {
                throw new Exception("O nome é inválido");
            }
            if (pessoa.Email == null || pessoa.Email == "")
            {
                throw new Exception("O Email é inválido");
            }
            if (pessoa.Fone == null || pessoa.Fone == "")
            {
                throw new Exception("O telefone é inválido");
            }
        }



    }




}

