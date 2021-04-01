using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Capa { get; set; }
        public string Sinopse { get; set; }
        public decimal Preco { get; set; }
        public int GeneroId { get; set; }

        public LivroModel(int id, string nome, string capa, string sinopse,int generoId,decimal preco)
        {
            Id = id;
            Titulo = nome;
            Capa = capa;
            Sinopse = sinopse;
            GeneroId = generoId;
            Preco = preco;
        }
    }
}
