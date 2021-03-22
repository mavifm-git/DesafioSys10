using Desafio.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{


    public class UsuarioViewModel
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Login { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public PerfilEnum Perfil { get; set; }


    }
}
