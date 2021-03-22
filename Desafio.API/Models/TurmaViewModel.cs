using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{


    public class TurmaViewModel
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int EscolaID { get; set; }


        public List<AlunoViewModel> Alunos { get; set; }



        public TurmaViewModel()
        {
            this.Alunos = new List<AlunoViewModel>();
        }
    }
}
