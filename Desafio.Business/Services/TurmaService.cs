using Desafio.Business.DTO;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace  Desafio.Business.Services
{

    public class TurmaService : ITurmaService
    {

        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<Turma> Adicionar(Turma turma)
        {
            await _turmaRepository.Adicionar(turma);
            return turma;
        }

        public async Task<Turma> Atualizar(Turma turma)
        {   
            await _turmaRepository.Atualizar(turma);
            return turma;
        }

        public async Task<IEnumerable<Turma>> Buscar(Expression<Func<Turma, bool>> predicate)
        {
            return await _turmaRepository.Buscar(predicate);
        }

        public async Task<Turma> BuscarPorID(int id)
        {
           return await _turmaRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Turma>> BuscarTodos()
        {
           return await _turmaRepository.ObterTodos();
        }

        public async Task<IEnumerable<Turma>> BuscarTodosPaginado(int pagina, int registros)
        {
            return await _turmaRepository.ObterTodosPaginados(pagina, registros);
        }

        public async Task Remover(int id)
        {
            var turma = await _turmaRepository.ObterPorId(id);

            await _turmaRepository.Remover(turma);
        }




        public void Dispose()
        {
            _turmaRepository.Dispose();
        }


    }


}