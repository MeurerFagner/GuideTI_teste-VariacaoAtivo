using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.DTO;

namespace VariacaoAtivo.Dominio.Interfaces
{
    public interface IYahooFinanceAPIService
    {
        Task<ConsultaAtivoResultDTO?> ConsultarVariacaoPrecoDeAtivo(string ativo);
    }
}
