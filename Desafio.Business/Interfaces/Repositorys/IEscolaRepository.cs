using Desafio.Business.Models;
using System.Threading.Tasks;

namespace Desafio.Business.Interfaces
{
    public interface IEscolaRepository : IRepository<Escola>
    {

        Task<Escola> ObterEscolaCompleta(int EscolaID);

    }
}
