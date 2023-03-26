using VariacaoAtivo.Dominio.Entidade;
using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;

namespace VariacaoAtivo.Infra.Repositorios
{
    public abstract class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : EntidadeBase
    {
        protected readonly Context _context;

        public RepositorioBase(Context context)
        {
            _context = context;
        }

        public async Task Delete(TEntity entity)
        {
            await Task.FromResult(_context.Set<TEntity>().Remove(entity));
        }

        public async Task Insert(TEntity entity)
        {
            await Task.FromResult(_context.Set<TEntity>().Add(entity));
        }

        public async Task Update(TEntity entity)
        {
            await Task.FromResult(_context.Set<TEntity>().Update(entity));
        }
    }
}
