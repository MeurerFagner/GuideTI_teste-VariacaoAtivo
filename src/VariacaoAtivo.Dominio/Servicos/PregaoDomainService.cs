using VariacaoAtivo.Dominio.DTO;
using VariacaoAtivo.Dominio.Entidade;
using VariacaoAtivo.Dominio.Helprers;
using VariacaoAtivo.Dominio.Interfaces.DomainService;
using VariacaoAtivo.Dominio.Interfaces.Infra;
using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;

namespace VariacaoAtivo.Dominio.Servicos
{
    public class PregaoDomainService : IPregaoDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IYahooFinanceAPIService _yahooFinanceAPIService;

        public PregaoDomainService(IUnitOfWork unitOfWork, IYahooFinanceAPIService yahooFinanceAPIService)
        {
            _unitOfWork = unitOfWork;
            _yahooFinanceAPIService = yahooFinanceAPIService;
        }

        public async Task AtualizarPregoesDoAtivo(string ativoSymbol)
        {
            var resultadoConsulta = await _yahooFinanceAPIService.ConsultarVariacaoPrecoDeAtivo(ativoSymbol);

            if (resultadoConsulta == null)
                return;

            var ativo = await _unitOfWork.Ativos.ObtemAtivoPeloSimbolo(ativoSymbol);

            if (ativo == null)
            {
                var meta = resultadoConsulta.chart.result[0].meta;
                ativo = new Ativo()
                {
                    Symbol = meta.symbol.ToUpper(),
                    Currency = meta.currency,
                    FirstDateTrade = DateTime.UnixEpoch.AddSeconds(meta.firstTradeDate),
                    DataCadastro = DateTime.Now
                };

                await _unitOfWork.Ativos.Insert(ativo);

                await _unitOfWork.Commit();
            }

            var pregoesTimetamp = resultadoConsulta.chart.result[0].timestamp;
            var pregoesQuote = resultadoConsulta.chart.result[0].indicators.quote[0];

            for (int i = 0; i < pregoesTimetamp.Length; i++)
            {
                if (!pregoesQuote.open[i].HasValue)
                    continue;

                var dataPregao = DateTimeHelper.ConvertTimestampToDateTime(pregoesTimetamp[i]);

                var pregao = await _unitOfWork.Pregoes.ObtemPorAtivoData(ativo.Id, dataPregao);

                if (pregao == null)
                {
                    pregao = new Pregao
                    {
                        IdAtivo = ativo.Id,
                        DataCadastro = DateTime.Now,
                        DataPregao = dataPregao,
                        Open = pregoesQuote.open[i],
                        Low = pregoesQuote.low[i],
                        High = pregoesQuote.high[i],
                        Close = pregoesQuote.close[i],
                        Volume = pregoesQuote.volume[i]
                    };

                    await _unitOfWork.Pregoes.Insert(pregao);
                }
                else
                {
                    pregao.Low = pregoesQuote.low[i];
                    pregao.High = pregoesQuote.high[i];
                    pregao.Close = pregoesQuote.close[i];
                    pregao.Volume = pregoesQuote.volume[i];

                    await _unitOfWork.Pregoes.Update(pregao);
                }
            }

            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<VariacaoPrecoDoAtivoDTO>> ConsultaVariacaoDoAtivoNosUltimosTrintaPregoes(string ativoSymbol)
        {
            var pregoesDTO = new List<VariacaoPrecoDoAtivoDTO>();

            var pregoes = await _unitOfWork.Pregoes.SelecionaUltimosTritaPregoesDoAtivo(ativoSymbol);

            for (int i = 0; i < pregoes.Count(); i++)
            {
                var pregao = pregoes.ElementAt(i);

                VariacaoPrecoDoAtivoDTO variacaoPrecoDoAtivoDTO = new()
                {
                    Dia = i + 1,
                    Data = pregao.DataPregao.ToString("dd/MM/yyyy"),
                    Currency = pregao.Ativo.Currency,
                    Valor = pregao.Open!.Value
                };

                if (i > 0)
                {
                    var valorDiaAnterior = pregoes.ElementAt(i - 1).Open!.Value;
                    var valorPrimeiroDia = pregoes.ElementAt(0).Open!.Value;

                    variacaoPrecoDoAtivoDTO.PrecentualVaricaoDiaAnterior = CalcularPercentualDeDiferenca(valorDiaAnterior, variacaoPrecoDoAtivoDTO.Valor);
                    variacaoPrecoDoAtivoDTO.PercentualVariacaoPrimeiraData = CalcularPercentualDeDiferenca(valorPrimeiroDia, variacaoPrecoDoAtivoDTO.Valor);
                }

                pregoesDTO.Add(variacaoPrecoDoAtivoDTO);
            }
            return pregoesDTO;
        }

        public float CalcularPercentualDeDiferenca(float primeiroValor, float segundoValor)
        {
            var percentual = (segundoValor - primeiroValor) / primeiroValor * 100;

            return percentual;
        }
    }
}
