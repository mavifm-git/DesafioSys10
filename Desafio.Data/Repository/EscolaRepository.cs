using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Desafio.Data.Repository
{
    public class EscolaRepository : Repository<Escola>, IEscolaRepository
    {
 
        public EscolaRepository(DesafioDbContext context) : base(context)
        {
            
        }


        public async Task<Escola> ObterEscolaCompleta(int EscolaID)
        {
            return await Db.Escolas.AsNoTracking()
                .Include(e => e.Turmas)
                    .ThenInclude(t => t.Alunos)
                .FirstOrDefaultAsync(e => e.Id == EscolaID);
        }

    }
}
