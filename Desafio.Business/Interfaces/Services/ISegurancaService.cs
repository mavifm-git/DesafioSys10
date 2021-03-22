using Desafio.Business.DTO;
using Desafio.Business.Models;
using System;



namespace Desafio.Business.Interfaces
{
    public interface ISegurancaService
    {
        string GenerateToken(Usuario user);
    }
}