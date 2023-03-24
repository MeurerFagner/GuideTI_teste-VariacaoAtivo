using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.Entidade;

namespace VariacaoAtivo.Infra.MapEntidades
{
    public class AtivoMap : IEntityTypeConfiguration<Ativo>
    {
        public void Configure(EntityTypeBuilder<Ativo> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasMany(m => m.Pregoes)
                .WithOne(o => o.Ativo)
                .HasForeignKey(f => f.IdAtivo);
        }
    }
}
