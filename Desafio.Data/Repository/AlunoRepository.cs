using Desafio.Business.DTO;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Data.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
 
        public AlunoRepository(DesafioDbContext context) : base(context)
        {
            
        }


        public async Task<List<NotaAlunoTurmaDTO>> ObterNotaAlunosTurma(int pagina, int registros)
        {
            return await Db.Alunos.AsNoTracking()
                .Include(a => a.Turma)
                .Skip(registros * (pagina - 1))
                .Take(registros)
                .Select(a=> new NotaAlunoTurmaDTO
                {
                    IdTurma = a.TurmaID,
                    NomeTurma = a.Turma.Nome,
                    NomeAluno = a.Nome,
                    NotaAluno = a.Nota
                })
                .OrderBy(a=>a.NomeTurma)
                .ThenBy(a=>a.NomeAluno)
                .ToListAsync();
        }


        public async Task<List<MediaTurmaDTO>> ObterMediaTurma()
        {
            return await Db.Alunos.AsNoTracking()
                .Include(a => a.Turma)
                .GroupBy(t => new { t.Turma.Id, t.Turma.Nome })
                .Select(a => new MediaTurmaDTO
                {
                    IdTurma = a.Key.Id,
                    NomeTurma = a.Key.Nome,
                    NotaMedia = Convert.ToDecimal(a.Sum(x => x.Nota) / a.Count())
                })
                .OrderByDescending(a => a.NotaMedia)
                .ToListAsync();
        }
    }
}
