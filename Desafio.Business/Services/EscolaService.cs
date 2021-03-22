using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace  Desafio.Business.Services
{

    public class EscolaService : IEscolaService
    {

        private readonly IEscolaRepository _escolaRepository;

        public EscolaService(IEscolaRepository escolaRepository)
        {
            _escolaRepository = escolaRepository;
        }

        public async Task<Escola> Adicionar(Escola escola)
        {
            await _escolaRepository.Adicionar(escola);
            return escola;
        }

        public async Task<Escola> Atualizar(Escola escola)
        {   
            await _escolaRepository.Atualizar(escola);
            return escola;
        }

        public async Task<IEnumerable<Escola>> Buscar(Expression<Func<Escola, bool>> predicate)
        {
            return await _escolaRepository.Buscar(predicate);
        }

        public async Task<Escola> BuscarPorID(int id)
        {
           return await _escolaRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Escola>> BuscarTodos()
        {
           return await _escolaRepository.ObterTodos();
        }

        public async Task<IEnumerable<Escola>> BuscarTodosPaginado(int pagina, int registros)
        {
            return await _escolaRepository.ObterTodosPaginados(pagina, registros);
        }


        public async Task<Escola> ObterEscolaCompleta(int EscolaID)
        {
            return await _escolaRepository.ObterEscolaCompleta(EscolaID);
        }


        public async Task Remover(int id)
        {
            var escola = await _escolaRepository.ObterPorId(id);

            await _escolaRepository.Remover(escola);
        }

        public void Dispose()
        {
            _escolaRepository.Dispose();
        }


    }


}