using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface ITurmaService : IDisposable
    {
        Task<IEnumerable<Turma>> Buscar(Expression<Func<Turma, bool>> predicate);
        Task<Turma> BuscarPorID(int id);
        Task<IEnumerable<Turma>> BuscarTodos();
        Task<IEnumerable<Turma>> BuscarTodosPaginado(int pagina, int registros);
        Task<Turma> Adicionar(Turma turma);
        Task<Turma> Atualizar(Turma turma);
        Task Remover(int id);


    }
}