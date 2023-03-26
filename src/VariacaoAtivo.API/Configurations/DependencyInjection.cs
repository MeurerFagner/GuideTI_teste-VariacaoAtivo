using VariacaoAtivo.APP;
using VariacaoAtivo.Dominio.Interfaces.APP;
using VariacaoAtivo.Dominio.Interfaces.DomainService;
using VariacaoAtivo.Dominio.Interfaces.Infra;
using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;
using VariacaoAtivo.Dominio.Servicos;
using VariacaoAtivo.Infra;
using VariacaoAtivo.Infra.Repositorios;

namespace VariacaoAtivo.API.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IYahooFinanceAPIService, YahooFinanceAPIService>();
            services.AddScoped<IAtivoRepositorio, AtivoRepositorio>();
            services.AddScoped<IPregaoRepositorio, PregaoRepositorio>();
            services.AddScoped<IAtivoRepositorio, AtivoRepositorio>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPregaoDomainService, PregaoDomainService>();

            services.AddScoped<IPregaoAPP, PregaoAPP>();

        }
    }
}
