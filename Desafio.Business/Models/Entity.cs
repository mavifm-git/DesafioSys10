using System;

namespace Desafio.Business.Models
{
    public abstract class Entity
    {  
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }


    }
}