using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class UsuarioMappings : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasColumnName("Nome");

            builder.Property(u => u.Login)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("Login");

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("Senha");

            builder.Property(u => u.Perfil)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("Perfil");




            builder.ToTable("Usuario");
        }


    }
}

