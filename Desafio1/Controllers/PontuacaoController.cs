using Desafio1.Models;
using Desafio1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PontuacaoController : Controller
    {
        

        private readonly PontuacaoService _service;
        public PontuacaoController(PontuacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {                
                return Ok(_service.GetAll());

            }catch (Exception ex)
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
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pontuacao novaPontuacao)
        {
            try
            {
                return Ok(_service.Save(novaPontuacao));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Pontuacao pontuacaoAtualizada)
        {
            try
            {
                return Ok(_service.Update(pontuacaoAtualizada));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("Pontuacao deletada com sucesso!");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
