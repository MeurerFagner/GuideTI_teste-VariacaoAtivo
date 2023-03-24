using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.Interfaces;
using VariacaoAtivo.Infra;

namespace VariacaoAtivo.Dominio.Teste
{
    public class YahooFinanceAPIServiceTest
    {
        private readonly YahooFinanceAPIService _yahooFinanceAPIService;

        public YahooFinanceAPIServiceTest()
        {
            _yahooFinanceAPIService = new();
        }

        [Fact]
        public async Task ConsultarVariacaoPrecoDeAtivo_SeAtivoValidoInformado_RetornaDTOComDadosEmChartResult()
        {
            // Arrange
            var ativo = "PETR4.SA";

            // Act
            var response = await _yahooFinanceAPIService.ConsultarVariacaoPrecoDeAtivo(ativo);

            // Assert
            Assert.NotNull(response?.chart?.result.FirstOrDefault());
            Assert.Equal(ativo, response.chart.result?.FirstOrDefault()?.meta.symbol);
        }

        [Fact]
        public async Task ConsultarVariacaoPrecoDeAtivo_SeAtivoInexistenteInformado_RetornaDTONull()
        {
            // Arrange
            var ativo = "ATIVO_INEXISTENTE";

            // Act
            var response = await _yahooFinanceAPIService.ConsultarVariacaoPrecoDeAtivo(ativo);

            // Assert
            Assert.Null(response);
        }

    }
}
