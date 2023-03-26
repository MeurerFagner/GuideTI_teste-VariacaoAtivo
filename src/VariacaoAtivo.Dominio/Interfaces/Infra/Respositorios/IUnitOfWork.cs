namespace VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios
{
    public interface IUnitOfWork : IDisposable
    {
        IAtivoRepositorio Ativos { get; }
        IPregaoRepositorio Pregoes { get; }

        Task Commit();

    }
}
