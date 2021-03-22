using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace  Desafio.Business.Services
{

    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            if (Buscar(x => x.Login == usuario.Login).Result.Any())
            {
                throw new Exception("Já Existe um usuário cadastrado com esse login.");
            }


            await _usuarioRepository.Adicionar(usuario);
            return usuario;
        }

        public async Task<Usuario> Atualizar(Usuario usuario)
        {   
            await _usuarioRepository.Atualizar(usuario);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> predicate)
        {
            return await _usuarioRepository.Buscar(predicate);
        }

        public async Task<Usuario> BuscarPorID(int id)
        {
           return await _usuarioRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
           return await _usuarioRepository.ObterTodos();
        }

        public async Task<IEnumerable<Usuario>> BuscarTodosPaginado(int pagina, int registros)
        {
            return await _usuarioRepository.ObterTodosPaginados(pagina, registros);
        }

        public async Task Remover(int id)
        {
            var Usuario = await _usuarioRepository.ObterPorId(id);

            await _usuarioRepository.Remover(Usuario);
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }


    }


}