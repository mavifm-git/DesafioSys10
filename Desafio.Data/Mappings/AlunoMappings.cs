using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class AlunoMappings : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasColumnName("Nome");

            builder.Property(a => a.Nota)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Nota");

            builder.Property(a => a.TurmaID)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("TurmaID");



            builder.ToTable("Aluno");
        }


    }
}

