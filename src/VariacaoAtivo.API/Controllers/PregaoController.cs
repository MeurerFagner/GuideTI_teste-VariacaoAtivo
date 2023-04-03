using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VariacaoAtivo.Dominio.Interfaces.APP;

namespace VariacaoAtivo.API.Controllers
{
    [Route("api/pregao")]
    [ApiController]
    public class PregaoController : ControllerBase
    {
        private readonly IPregaoAPP _pregaoApp;

        public PregaoController(IPregaoAPP pregaoApp)
        {
            _pregaoApp = pregaoApp;
        }

        [HttpGet,Route("consulta_varicao_de_preco/{ativo}")]
        public async Task<IActionResult> ConsultaVaricaoDePrecoDoAtivo(string ativo)
        {
            try
            {
                var resultado = await _pregaoApp.ConsultaVariacaoDePrecoDoAtivo(ativo);

                if (resultado == null)
                    return NotFound("Ativo informado não encontrado.");

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.GetType().Name,
                    message = ex.Message,
                    stackTrace = ex.StackTrace,
                    inerrException = ex.InnerException == null? null : ex.InnerException.Message

                });
            }
        }
    }
}
