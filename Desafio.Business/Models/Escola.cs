using System.Collections.Generic;

namespace Desafio.Business.Models
{
    public class Escola : Entity
    {  
        public string Nome { get; set; }




        /* EF Relations */
        public virtual IList<Turma> Turmas { get; set; }
    }
}