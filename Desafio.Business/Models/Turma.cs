using System.Collections.Generic;
namespace Desafio.Business.Models
{
    public class Turma : Entity
    {  
        public string Nome { get; set; }

        public int EscolaID { get; set; }





        /* EF Relations */
        public virtual Escola Escola { get; set; }

        public virtual IList<Aluno> Alunos { get; set; }
    }
}