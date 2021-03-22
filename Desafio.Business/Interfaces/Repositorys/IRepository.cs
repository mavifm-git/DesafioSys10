
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);       
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(int id);
        Task<List<TEntity>> ObterTodos();
        Task<List<TEntity>> ObterTodosPaginados(int pagina, int registros);
        Task Atualizar(TEntity entity);
        Task Remover(TEntity entity);
        Task<int> SaveChanges();
    }
}