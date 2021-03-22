using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }
    }
}
