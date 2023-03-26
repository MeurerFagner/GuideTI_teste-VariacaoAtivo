using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.Servicos;
using Moq;
using Moq.AutoMock;
using Xunit;
using Bogus;
using VariacaoAtivo.Dominio.Entidade;
using VariacaoAtivo.Dominio.DTO;
using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;
using VariacaoAtivo.Dominio.Interfaces.Infra;

namespace VariacaoAtivo.Dominio.Teste
{
    public class PregaoDomainServiceTest
    {
        private const string ATIVO_PETR4SA = "PETR4.SA";
        private readonly PregaoDomainService _pregaoDomainService;
        private readonly AutoMocker _mocker;

        public PregaoDomainServiceTest()
        {
            _mocker = new();
            _pregaoDomainService = _mocker.CreateInstance<PregaoDomainService>();

        }

        #region Teste do Método "AtualizarPregoesDoAtivo"
        [Fact]
        public async Task AtualizarPregoesDoAtivo_DadoQueNaoEncontraAtivoNaConsultaAoYahooFinance_DeveInterromperOProcesso()
        {
            // Arrange
            _mocker.GetMock<IYahooFinanceAPIService>()
                .Setup(s => s.ConsultarVariacaoPrecoDeAtivo(It.IsAny<string>()))
                .ReturnsAsync((ConsultaAtivoResultDTO?)null);

            // Act
            await _pregaoDomainService.AtualizarPregoesDoAtivo(ATIVO_PETR4SA);

            // Assert
            _mocker.GetMock<IUnitOfWork>()
                .Verify(s => s.Ativos.ObtemAtivoPeloSimbolo(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task AtualizarPregoesDoAtivo_DadoQueNaoExisteRegistroDoAtivo_DeveRealizarInclusaoDoAtivo()
        {
            // Arrange

            _mocker.GetMock<IYahooFinanceAPIService>()
                .Setup(s => s.ConsultarVariacaoPrecoDeAtivo(It.IsAny<string>()))
                .ReturnsAsync(GerarConsultaAtivoResultDTOFake(30));

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Ativos.ObtemAtivoPeloSimbolo(It.IsAny<string>()))
                .ReturnsAsync((Ativo?)null);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Pregoes.ObtemPorAtivoData(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Pregao() { });

            // Act
            await _pregaoDomainService.AtualizarPregoesDoAtivo(ATIVO_PETR4SA);

            // Assert
            _mocker.GetMock<IUnitOfWork>()
                .Verify(s => s.Ativos.Insert(It.IsAny<Ativo>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarPregoesDoAtivo_DadoQueExisteRegistroDoAtivo_NaoDeveRealizarInclusaoDoAtivo()
        {
            // Arrange
            _mocker.GetMock<IYahooFinanceAPIService>()
                .Setup(s => s.ConsultarVariacaoPrecoDeAtivo(It.IsAny<string>()))
                .ReturnsAsync(GerarConsultaAtivoResultDTOFake(30, ATIVO_PETR4SA));

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Ativos.ObtemAtivoPeloSimbolo(It.IsAny<string>()))
                .ReturnsAsync(new Ativo()
                {
                    Id = 1,
                    Symbol = ATIVO_PETR4SA
                });

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Pregoes.ObtemPorAtivoData(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Pregao() { });

            // Act
            await _pregaoDomainService.AtualizarPregoesDoAtivo(ATIVO_PETR4SA);

            // Assert
            _mocker.GetMock<IUnitOfWork>()
                .Verify(s => s.Ativos.Insert(It.IsAny<Ativo>()), Times.Never);
        }

        [Fact]
        public async Task AtualizarPregoesDoAtivo_DadoQueTodosPregoesRecebidoSaoNovos_DeveIncluirTodosPregoes()
        {
            var quantidadePregoes = 30;
            var consultaResult = GerarConsultaAtivoResultDTOFake(quantidadePregoes, ATIVO_PETR4SA);

            Ativo ativo = new()
            {
                Id = 1,
                Symbol = ATIVO_PETR4SA,
                Currency = "BRL"
            };

            _mocker.GetMock<IYahooFinanceAPIService>()
                .Setup(s => s.ConsultarVariacaoPrecoDeAtivo(It.IsAny<string>()))
                .ReturnsAsync(consultaResult);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Ativos.ObtemAtivoPeloSimbolo(It.IsAny<string>()))
                .ReturnsAsync(ativo);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Pregoes.ObtemPorAtivoData(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync((Pregao?)null);

            // Act
            await _pregaoDomainService.AtualizarPregoesDoAtivo(ATIVO_PETR4SA);

            // Assert
            _mocker.GetMock<IUnitOfWork>()
                .Verify(v => v.Pregoes.Insert(It.IsAny<Pregao>()), Times.Exactly(quantidadePregoes));

        }

        [Fact]
        public async Task AtualizarPregoesDoAtivo_DadoQueJaExisteRegistradoDosPregoesRecebido_NaoDeveIncluirPregoes()
        {
            var quantidadePregoes = 30;
            var consultaResult = GerarConsultaAtivoResultDTOFake(quantidadePregoes, ATIVO_PETR4SA);

            Ativo ativo = new()
            {
                Id = 1,
                Symbol = ATIVO_PETR4SA,
                Currency = "BRL"
            };

            _mocker.GetMock<IYahooFinanceAPIService>()
                .Setup(s => s.ConsultarVariacaoPrecoDeAtivo(It.IsAny<string>()))
                .ReturnsAsync(consultaResult);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Ativos.ObtemAtivoPeloSimbolo(It.IsAny<string>()))
                .ReturnsAsync(ativo);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(s => s.Pregoes.ObtemPorAtivoData(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Pregao() { });

            // Act
            await _pregaoDomainService.AtualizarPregoesDoAtivo(ATIVO_PETR4SA);

            // Assert
            _mocker.GetMock<IUnitOfWork>()
                .Verify(v => v.Pregoes.Insert(It.IsAny<Pregao>()), Times.Never);

        }

        #endregion

        #region Teste do Método ""

        #endregion

        private ConsultaAtivoResultDTO GerarConsultaAtivoResultDTOFake(int quantidadePregoes, string ativo = null)
        {
            ConsultaAtivoResultDTO consultaAtivoResultDTO = new()
            {
                chart = new()
            };

            Faker faker = new();
            Faker<Meta> fakerMeta = new Faker<Meta>()
                .RuleFor(c => c.symbol, f => ativo ?? f.Finance.Random.String(5).ToUpper())
                .RuleFor(c => c.currency, f => f.Finance.Currency().Code)
                .RuleFor(c => c.firstTradeDate, f => f.Date.PastOffset().Second);

            var timeStamp = new int[quantidadePregoes];
            var volume = new long?[quantidadePregoes];
            var open = new float?[quantidadePregoes];
            var low = new float?[quantidadePregoes];
            var high = new float?[quantidadePregoes];
            var close = new float?[quantidadePregoes];

            for (int i = 0; i < quantidadePregoes; i++)
            {
                timeStamp[i] = (int)DateTimeOffset.Now.AddDays(i + 1 - quantidadePregoes).ToUnixTimeSeconds();
                volume[i] = faker.Random.Int(min: 1);
                open[i] = faker.Random.Float(min: 1, max: 100);
                low[i] = faker.Random.Float(min: 1, max: 100);
                high[i] = faker.Random.Float(min: 1, max: 100);
                close[i] = faker.Random.Float(min: 1, max: 100);
            }

            Result[] results = {
                new Result
                {
                    meta = fakerMeta.Generate(1).First(),
                    timestamp = timeStamp,
                    indicators = new()
                    {
                        quote =  new Quote[]
                        {
                            new Quote
                            {
                                open = open,
                                low = low,
                                high = high,
                                close = close,
                                volume = volume
                            }
                        }
                    }
                }

            };

            consultaAtivoResultDTO.chart.result = results;

            return consultaAtivoResultDTO;
        }



    }
}
