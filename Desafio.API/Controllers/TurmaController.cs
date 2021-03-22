using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.API.Models;
using Desafio.Business.DTO;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Desafio.API.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TurmaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITurmaService _turmaService;


        public TurmaController(ITurmaService TurmaService, IMapper mapper)
        {
            _mapper = mapper;
            _turmaService = TurmaService;
        }

        
        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TurmaViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TurmaViewModel>>> ObterTodos()
        {
            try
            {
                var listaTurmas = await _turmaService.BuscarTodos();

                return Ok(_mapper.Map<IEnumerable<TurmaViewModel>>(listaTurmas));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterTodosPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TurmaViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TurmaViewModel>>> ObterTodosPaginado([FromQuery] int pagina = 1, [FromQuery] int qtdRegistros = 5)
        {

            try
            {
                var listaTurmas = await _turmaService.BuscarTodosPaginado(pagina, qtdRegistros);

                return Ok(_mapper.Map<IEnumerable<TurmaViewModel>>(listaTurmas));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TurmaViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TurmaViewModel>> ObterPorId(int id)
        {
            TurmaViewModel model;

            try
            {
                var turma = await _turmaService.BuscarPorID(id);

                if(turma == null) return NotFound();

                model = _mapper.Map<TurmaViewModel>(turma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TurmaViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Adicionar(TurmaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var turma = _mapper.Map<Turma>(model);

            try
            {               
                await _turmaService.Adicionar(turma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = turma.Id }, model);
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(TurmaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!_turmaService.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var turma = _mapper.Map<Turma>(model);
                await _turmaService.Atualizar(turma);
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
            if (!_turmaService.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _turmaService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }




        [HttpGet("ObterMediaTurma")]
        [Authorize(Roles = "Escola")]
        [Authorize(Roles = "Professor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MediaTurmaDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MediaTurmaDTO>>> ObterMediaTurma([FromQuery] int pagina = 1, [FromQuery] int qtdRegistros = 5)
        {
            var usuarioID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var listaTurmas = await _turmaService.BuscarTodosPaginado(pagina, qtdRegistros);

                return Ok(listaTurmas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}