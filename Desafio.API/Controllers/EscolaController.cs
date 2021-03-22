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


namespace Desafio.API.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class EscolaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEscolaService _escolaService;


        public EscolaController(IEscolaService escolaService, IMapper mapper)
        {
            _mapper = mapper;
            _escolaService = escolaService;
        }



        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EscolaViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EscolaViewModel>>> ObterTodos()
        {
            try
            {

                var listaEscolas = await _escolaService.BuscarTodos();

                return Ok(_mapper.Map<IEnumerable<EscolaViewModel>>(listaEscolas));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterTodosPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EscolaViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EscolaViewModel>>> ObterTodosPaginado([FromQuery] int pagina = 1, [FromQuery] int qtdRegistros = 5)
        {

            try
            {
                var listaEscolas = await _escolaService.BuscarTodosPaginado(pagina, qtdRegistros);

                return Ok(_mapper.Map<IEnumerable<EscolaViewModel>>(listaEscolas));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EscolaViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EscolaViewModel>> ObterPorId(int id)
        {
            EscolaViewModel model;

            try
            {
                var escola = await _escolaService.BuscarPorID(id);

                if(escola == null) return NotFound();

                model = _mapper.Map<EscolaViewModel>(escola);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }


        [HttpGet("ObterCompleto/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EscolaViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EscolaViewModel>> ObterCompleto(int id)
        {
            EscolaViewModel model;

            try
            {
                var escola = await _escolaService.ObterEscolaCompleta(id);

                if (escola == null) return NotFound();

                model = _mapper.Map<EscolaViewModel>(escola);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EscolaViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Adicionar(EscolaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var escola = _mapper.Map<Escola>(model);
            try
            {               
                await _escolaService.Adicionar(escola);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = escola.Id }, model);
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(EscolaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!_escolaService.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var escola = _mapper.Map<Escola>(model);
                await _escolaService.Atualizar(escola);
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
            if (!_escolaService.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _escolaService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }




    }


}