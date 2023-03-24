using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.DTO;
using VariacaoAtivo.Dominio.Interfaces;

namespace VariacaoAtivo.Infra
{
    public  class YahooFinanceAPIService : IYahooFinanceAPIService
    {
        private const string CONSULTA_ATIVO_URL = @"https://query2.finance.yahoo.com/v8/finance/chart/";

        public async Task<ConsultaAtivoResultDTO?> ConsultarVariacaoPrecoDeAtivo(string ativo)
        {
            using var httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync(CONSULTA_ATIVO_URL + ativo.ToUpper());

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var response = await httpResponse.Content.ReadAsStringAsync();

            var consultaRetorno = JsonConvert.DeserializeObject<ConsultaAtivoResultDTO>(response);

            return consultaRetorno;
        }
    }
}
