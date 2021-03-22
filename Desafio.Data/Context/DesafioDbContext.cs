using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Data.Context
{
    public class DesafioDbContext: DbContext
    {


        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options)
        {

        }

      
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime))))
                property.SetColumnType("datetime");


            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Administrador",
                Login = "admin",
                Senha = "1234",
                Perfil = Business.Enums.PerfilEnum.Escola
            });

            modelBuilder.Entity<Escola>().HasData(new Escola
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Pedro II"
            });

            modelBuilder.Entity<Turma>().HasData(new Turma
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "101",
                EscolaID = 1
            });

            modelBuilder.Entity<Aluno>().HasData(new Aluno
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Marcus Vinicius",
                Nota = 10,
                TurmaID = 1
            });


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioDbContext).Assembly);

//            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCriacao").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}