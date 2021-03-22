using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.API.Models;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Desafio.API.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _config;


        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
            _config = configuration;
        }

        
        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> ObterTodos()
        {
            try
            {
                var listaUsuarios = await _usuarioService.BuscarTodos();

                return Ok(_mapper.Map<IEnumerable<UsuarioViewModel>>(listaUsuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterTodosPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> ObterTodosPaginado([FromQuery] int pagina = 1, [FromQuery] int qtdRegistros = 5)
        {

            try
            {
                var listaUsuarioes = await _usuarioService.BuscarTodosPaginado(pagina, qtdRegistros);

                return Ok(_mapper.Map<IEnumerable<UsuarioViewModel>>(listaUsuarioes));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioViewModel>> ObterPorId(int id)
        {
            UsuarioViewModel model;

            try
            {
                var usuario = await _usuarioService.BuscarPorID(id);

                if(usuario == null) return NotFound();

                model = _mapper.Map<UsuarioViewModel>(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Adicionar(UsuarioViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var usuario = _mapper.Map<Usuario>(model);
            try
            {               
                await _usuarioService.Adicionar(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, model);
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(UsuarioViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!_usuarioService.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var Usuario = _mapper.Map<Usuario>(model);
                await _usuarioService.Atualizar(Usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Accepted(nameof(ObterPorId), new { id = model.Id });
        }


        [HttpDelete("Excluir/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            if (!_usuarioService.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _usuarioService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }



    }


}