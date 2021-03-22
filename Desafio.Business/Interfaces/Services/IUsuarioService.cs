using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> predicate);
        Task<Usuario> BuscarPorID(int id);
        Task<IEnumerable<Usuario>> BuscarTodos();
        Task<IEnumerable<Usuario>> BuscarTodosPaginado(int pagina, int registros);
        Task<Usuario> Adicionar(Usuario usuario);
        Task<Usuario> Atualizar(Usuario usuario);
        Task Remover(int id);


    }
}