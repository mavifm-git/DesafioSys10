using System.Linq;
using Desafio.API.Models;
using Desafio.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Desafio.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SegurancaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISegurancaService _segurancaService;



        public SegurancaController(IUsuarioService usuarioService, ISegurancaService segurancaService)
        {
            _usuarioService = usuarioService;
            _segurancaService = segurancaService;

        }



        [HttpPost("Entrar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Entrar([FromBody] LoginViewModel model)
        {
            bool resultado = ValidarUsuario(model);

            if (resultado)
            {
                var usuario = _usuarioService.Buscar(x => x.Login == model.Login && x.Senha == model.Senha).Result.FirstOrDefault();
                var tokenString = _segurancaService.GenerateToken(usuario);

                return Ok(new TokenViewModel { Token = tokenString });
            }
            else
            {
                return BadRequest("Usuário e senha inválidos");
            }
        }


        private bool ValidarUsuario(LoginViewModel model)
        {
            if (_usuarioService.Buscar(x => x.Login == model.Login && x.Senha == model.Senha).Result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }


}