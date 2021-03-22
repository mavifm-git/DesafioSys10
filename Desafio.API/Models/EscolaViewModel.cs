using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{


    public class EscolaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }


        public List<TurmaViewModel> Turmas { get; set; }

        public EscolaViewModel()
        {
            this.Turmas = new List<TurmaViewModel>();
        }

    }
}
