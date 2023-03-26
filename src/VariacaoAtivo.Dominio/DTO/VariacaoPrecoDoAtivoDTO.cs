using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariacaoAtivo.Dominio.DTO
{
    public class VariacaoPrecoDoAtivoDTO
    {
        public int Dia { get; set; }
        public string Data { get; set; }
        public string Currency { get; set; }
        public float Valor { get; set; }
        public float? PrecentualVaricaoDiaAnterior { get; set; }
        public float? PercentualVariacaoPrimeiraData { get; set; }
    }
}
