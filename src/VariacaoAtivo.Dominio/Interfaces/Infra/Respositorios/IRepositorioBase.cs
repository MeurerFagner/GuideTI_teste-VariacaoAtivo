using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.Entidade;

namespace VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios
{
    public interface IRepositorioBase<TEntity> where TEntity : EntidadeBase
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
