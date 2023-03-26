using VariacaoAtivo.Dominio.Entidade;

namespace VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios
{
    public interface IAtivoRepositorio : IRepositorioBase<Ativo>
    {
        Task<Ativo?> ObtemAtivoPeloSimbolo(string symbol);
    }
}
