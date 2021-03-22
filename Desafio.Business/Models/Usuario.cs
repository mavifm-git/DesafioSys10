using Desafio.Business.Enums;

namespace Desafio.Business.Models
{
    public class Usuario : Entity
    {  
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public PerfilEnum Perfil { get; set; }


    }
}