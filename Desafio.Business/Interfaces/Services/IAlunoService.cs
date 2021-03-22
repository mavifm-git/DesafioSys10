using Desafio.Business.DTO;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface IAlunoService : IDisposable
    {
        Task<IEnumerable<Aluno>> Buscar(Expression<Func<Aluno, bool>> predicate);
        Task<Aluno> BuscarPorID(int id);
        Task<IEnumerable<Aluno>> BuscarTodos();
        Task<IEnumerable<Aluno>> BuscarTodosPaginado(int pagina, int registros);
        Task<Aluno> Adicionar(Aluno aluno);
        Task<Aluno> Atualizar(Aluno aluno);
        Task Remover(int id);
        Task<List<NotaAlunoTurmaDTO>> BuscarNotaAlunosTurma(int pagina, int registros);
        Task<List<MediaTurmaDTO>> BuscarMediaTurma();
    }
}