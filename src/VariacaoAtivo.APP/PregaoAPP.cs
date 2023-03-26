using VariacaoAtivo.Dominio.DTO;
using VariacaoAtivo.Dominio.Interfaces.APP;
using VariacaoAtivo.Dominio.Interfaces.DomainService;

namespace VariacaoAtivo.APP
{
    public class PregaoAPP: IPregaoAPP
    {
        private readonly IPregaoDomainService _pregaoDomainService;

        public PregaoAPP(IPregaoDomainService pregaoDomainService)
        {
            _pregaoDomainService = pregaoDomainService;
        }

        public async Task<IEnumerable<VariacaoPrecoDoAtivoDTO>> ConsultaVariacaoDePrecoDoAtivo(string ativoSymbol)
        {
            await _pregaoDomainService.AtualizarPregoesDoAtivo(ativoSymbol);

            var resultado = await _pregaoDomainService.ConsultaVariacaoDoAtivoNosUltimosTrintaPregoes(ativoSymbol);

            return resultado;   
        }

    }
}
