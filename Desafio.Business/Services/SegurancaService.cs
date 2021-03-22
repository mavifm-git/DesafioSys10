using Desafio.Business.Interfaces;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Desafio.Business.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace  Desafio.Business.Services
{

   
    public class SegurancaService : ISegurancaService
    {

        private readonly IConfiguration _config;

        public SegurancaService(IConfiguration config)
        {
            _config = config;
        }


        public string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Nome.ToString()),
                    new Claim(ClaimTypes.Role, user.Perfil.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




    }


}