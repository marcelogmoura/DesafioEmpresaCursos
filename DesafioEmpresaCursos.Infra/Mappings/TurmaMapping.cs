using DesafioEmpresaCursos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioEmpresaCursos.Infra.Mappings
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.NumeroTurma)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(t => t.AnoLetivo)
                .IsRequired();
        }
    }
}
