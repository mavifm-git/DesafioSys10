using AutoMapper;
using Desafio.API.Models;
using Desafio.Business.Models;

namespace Desafio.API.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Escola, EscolaViewModel>().ReverseMap();
            CreateMap<Turma, TurmaViewModel>().ReverseMap();
            CreateMap<Aluno, AlunoViewModel>().ReverseMap();

        }
    }
}
