namespace Desafio.Business.Models
{
    public class Aluno : Entity
    {  
        public string Nome { get; set; }

        public decimal Nota { get; set; }

        public int TurmaID { get; set; }


        /* EF Relations */
        public virtual Turma Turma { get; set; }
    }
}