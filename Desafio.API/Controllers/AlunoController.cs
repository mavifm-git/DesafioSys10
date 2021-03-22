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
    public class AlunoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAlunoService _alunoService;


        public AlunoController(IAlunoService alunoService, IMapper mapper)
        {
            _mapper = mapper;
            _alunoService = alunoService;
        }

        
        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AlunoViewModel>>> ObterTodos()
        {
            try
            {
                var listaAlunos = await _alunoService.BuscarTodos();

                return Ok(_mapper.Map<IEnumerable<AlunoViewModel>>(listaAlunos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterTodosPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AlunoViewModel>>> ObterTodosPaginado([FromQuery] int pagina = 1, [FromQuery] int qtdRegistros = 5)
        {

            try
            {
                var listaAlunos = await _alunoService.BuscarTodosPaginado(pagina, qtdRegistros);

                return Ok(_mapper.Map<IEnumerable<AlunoViewModel>>(listaAlunos));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlunoViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AlunoViewModel>> ObterPorId(int id)
        {
            AlunoViewModel model;

            try
            {
                var aluno = await _alunoService.BuscarPorID(id);

                if(aluno == null) return NotFound();

                model = _mapper.Map<AlunoViewModel>(aluno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AlunoViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Adicionar(AlunoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var aluno = _mapper.Map<Aluno>(model);

            try
            {               
                await _alunoService.Adicionar(aluno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = aluno.Id }, model);
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(AlunoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!_alunoService.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var aluno = _mapper.Map<Aluno>(model);
                await _alunoService.Atualizar(aluno);
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
            if (!_alunoService.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _alunoService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }


        [HttpGet("ObterNotaAlunosTurma")]
        [Authorize(Roles = "Escola,Professor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotaAlunoTurmaDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<NotaAlunoTurmaDTO>>> ObterNotaAlunosTurma([FromQuery] int pagina = 1, [FromQuery] int qtdRegistros = 5)
        {
            //Recupera o ID do Usuario Logado
            var usuarioID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var listaAlunosNotaTurma = await _alunoService.BuscarNotaAlunosTurma(pagina, qtdRegistros);

                return Ok(listaAlunosNotaTurma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterMediaTurma")]
        [Authorize(Roles = "Escola,Professor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MediaTurmaDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<MediaTurmaDTO>>> ObterMediaTurma()
        {
            //Recupera o ID do Usuario Logado
            var usuarioID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var listaMediaTurma = await _alunoService.BuscarMediaTurma();

                return Ok(listaMediaTurma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}