using Desafio.Business.DTO;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace  Desafio.Business.Services
{

    public class AlunoService : IAlunoService
    {

        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<Aluno> Adicionar(Aluno aluno)
        {
            await _alunoRepository.Adicionar(aluno);
            return aluno;
        }

        public async Task<Aluno> Atualizar(Aluno aluno)
        {   
            await _alunoRepository.Atualizar(aluno);
            return aluno;
        }

        public async Task<IEnumerable<Aluno>> Buscar(Expression<Func<Aluno, bool>> predicate)
        {
            return await _alunoRepository.Buscar(predicate);
        }

        public async Task<Aluno> BuscarPorID(int id)
        {
           return await _alunoRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Aluno>> BuscarTodos()
        {
           return await _alunoRepository.ObterTodos();
        }

        public async Task<IEnumerable<Aluno>> BuscarTodosPaginado(int pagina, int registros)
        {
            return await _alunoRepository.ObterTodosPaginados(pagina, registros);
        }

        public async Task<List<NotaAlunoTurmaDTO>> BuscarNotaAlunosTurma(int pagina, int registros)
        {
            return await _alunoRepository.ObterNotaAlunosTurma(pagina, registros);
        }


        public async Task<List<MediaTurmaDTO>> BuscarMediaTurma()
        {
            return await _alunoRepository.ObterMediaTurma();
        }


        public async Task Remover(int id)
        {
            var aluno = await _alunoRepository.ObterPorId(id);

            await _alunoRepository.Remover(aluno);
        }

        public void Dispose()
        {
            _alunoRepository.Dispose();
        }


    }


}