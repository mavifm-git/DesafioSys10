using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class EscolaMappings : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasColumnName("Nome");




            // 1 : N => Escola : Turmas
            builder.HasMany(e => e.Turmas)
                .WithOne(t => t.Escola)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(t => t.EscolaID);



            builder.ToTable("Escola");
        }


    }
}

