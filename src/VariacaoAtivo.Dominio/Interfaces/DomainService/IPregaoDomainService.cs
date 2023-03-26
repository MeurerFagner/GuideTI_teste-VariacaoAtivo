using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.DTO;

namespace VariacaoAtivo.Dominio.Interfaces.DomainService
{
    public interface IPregaoDomainService
    {
        Task AtualizarPregoesDoAtivo(string ativoSymbol);
        Task<IEnumerable<VariacaoPrecoDoAtivoDTO>> ConsultaVariacaoDoAtivoNosUltimosTrintaPregoes(string ativoSymbol);
    }
}
