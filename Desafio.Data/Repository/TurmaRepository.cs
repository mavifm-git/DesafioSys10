using Desafio.Business.DTO;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Data.Repository
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
 
        public TurmaRepository(DesafioDbContext context) : base(context)
        {
            
        }



    }
}
