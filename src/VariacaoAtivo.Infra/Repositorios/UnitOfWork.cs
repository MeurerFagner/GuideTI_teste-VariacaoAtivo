using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;

namespace VariacaoAtivo.Infra.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly Context _context;

        public UnitOfWork(Context context, IAtivoRepositorio ativos, IPregaoRepositorio pregoes)
        {
            _context = context;
            Ativos = ativos;
            Pregoes = pregoes;
        }

        public IAtivoRepositorio Ativos { get;  }

        public IPregaoRepositorio Pregoes { get; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
