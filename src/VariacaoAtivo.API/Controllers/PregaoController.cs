using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VariacaoAtivo.Dominio.Interfaces.APP;

namespace VariacaoAtivo.API.Controllers
{
    [Route("api/[controller]")]
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
                return BadRequest(ex.Message);
            }
        }
    }
}
