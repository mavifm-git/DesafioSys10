using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class TurmaMappings : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasColumnName("Nome");


            builder.Property(t => t.EscolaID)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("EscolaID");


            // 1 : N => Turma : Alunos
            builder.HasMany(t => t.Alunos)
                .WithOne(a => a.Turma)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.TurmaID);


            builder.ToTable("Turma");
        }


    }
}

