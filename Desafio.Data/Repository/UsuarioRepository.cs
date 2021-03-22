using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;


namespace Desafio.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
 
        public UsuarioRepository(DesafioDbContext context) : base(context)
        {
            
        }

    }
}
