using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;

namespace Desafio.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DesafioDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DesafioDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }


        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        
        public virtual async Task<TEntity> ObterPorId(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }


        public virtual async Task<List<TEntity>> ObterTodosPaginados(int pagina, int registros)
        {
            return await DbSet
                .Skip(registros * (pagina-1))
                .Take(registros)
                .ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
