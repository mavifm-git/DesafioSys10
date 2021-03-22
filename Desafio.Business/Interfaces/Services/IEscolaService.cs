using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface IEscolaService : IDisposable
    {
        Task<IEnumerable<Escola>> Buscar(Expression<Func<Escola, bool>> predicate);
        Task<Escola> BuscarPorID(int id);
        Task<IEnumerable<Escola>> BuscarTodos();
        Task<IEnumerable<Escola>> BuscarTodosPaginado(int pagina, int registros);
        Task<Escola> Adicionar(Escola escola);
        Task<Escola> Atualizar(Escola escola);
        Task Remover(int id);

        Task<Escola> ObterEscolaCompleta(int EscolaID);
    }
}