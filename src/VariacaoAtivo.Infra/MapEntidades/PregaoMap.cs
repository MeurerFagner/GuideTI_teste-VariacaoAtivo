using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VariacaoAtivo.Dominio.Entidade;

namespace VariacaoAtivo.Infra.MapEntidades
{
    public class PregaoMap : IEntityTypeConfiguration<Pregao>
    {
        public void Configure(EntityTypeBuilder<Pregao> builder)
        {
            builder.ToTable("Pregoes", "WEB_API");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(o => o.Ativo)
                .WithMany(m => m.Pregoes)
                .HasForeignKey(f => f.IdAtivo);
        }
    }
}
