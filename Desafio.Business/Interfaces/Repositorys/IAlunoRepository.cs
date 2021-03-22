using Desafio.Business.DTO;
using Desafio.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Business.Interfaces
{
    public interface IAlunoRepository : IRepository<Aluno>
    {

        Task<List<NotaAlunoTurmaDTO>> ObterNotaAlunosTurma(int pagina, int registros);

        Task<List<MediaTurmaDTO>> ObterMediaTurma();

    }
}
