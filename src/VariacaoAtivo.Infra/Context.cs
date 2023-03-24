using Microsoft.EntityFrameworkCore;
using VariacaoAtivo.Dominio.Entidade;

namespace VariacaoAtivo.Infra
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options)
           : base(options) { }

        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Pregao> Pregoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}
