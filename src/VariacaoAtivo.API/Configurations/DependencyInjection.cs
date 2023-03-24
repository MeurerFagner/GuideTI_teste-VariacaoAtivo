using VariacaoAtivo.Dominio.Interfaces;
using VariacaoAtivo.Infra;

namespace VariacaoAtivo.API.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IYahooFinanceAPIService, YahooFinanceAPIService>();
        }
    }
}
