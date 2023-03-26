using VariacaoAtivo.Dominio.Entidade;

namespace VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios
{
    public interface IPregaoRepositorio : IRepositorioBase<Pregao>
    {
        Task<Pregao?> ObtemPorAtivoData(int id, DateTime dataPregao);
        Task<IEnumerable<Pregao>> SelecionaUltimosTritaPregoesDoAtivo(string ativo);
    }
}
