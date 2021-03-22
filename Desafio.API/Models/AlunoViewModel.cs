using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{


    public class AlunoViewModel
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int TurmaID { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Nota { get; set; }



    }
}
