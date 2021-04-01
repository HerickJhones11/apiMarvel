using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class GeneroModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
      
        public GeneroModel(int id,string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
