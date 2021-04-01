using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("livro")]
    public class LivroController : ControllerBase
    {
        private const string stringConnection = @"Server=54.39.68.137,1433;Database=BancoMarvel;User=herick;Password=Oloco*123;";

        public LivroController()
        {

        }

        [HttpGet]
        public IActionResult ObterLivros()
        {
            var livroModel = new List<LivroModel>();
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var query = @"select id,titulo, capa, Sinopse, generoId, Preco from livro";
            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                livroModel.Add(new LivroModel((int)reader[0], reader[1].ToString(), reader[2].ToString(),reader[3].ToString(),(int)reader[4],(decimal)reader[5]));
            }
            conn.Close();

            return Ok(livroModel);
        }

        [HttpGet("{id}")]
        public IActionResult ObterLivroPorId(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddLivro([FromBody] LivroModel livro)
        {
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var queryGenero = "select * from genero";
            SqlCommand commandGenero = new SqlCommand(queryGenero, conn);
            SqlDataReader reader = commandGenero.ExecuteReader();
            if (!reader.Read()) return Ok("Não existe genero cadastrado!");
            conn.Close();

            conn.Open();
            var query = @"insert into livro (titulo, capa, sinopse,generoId,Preco) values (@titulo, @capa, @sinopse, @generoId, @price)";
            SqlCommand command = new SqlCommand(query, conn);
             
            command.Parameters.AddWithValue("@generoId", livro.GeneroId);
            command.Parameters.AddWithValue("@titulo", livro.Titulo);
            command.Parameters.AddWithValue("@capa", livro.Capa);
            command.Parameters.Add(new SqlParameter("@sinopse", livro.Sinopse));
            command.Parameters.Add(new SqlParameter("@price", livro.Preco));

            command.ExecuteNonQuery();

            conn.Close();

            return Ok("Livro adicionado!");
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromBody] LivroModel livro)
        {
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var queryGenero = "select * from genero";
            SqlCommand commandGenero = new SqlCommand(queryGenero, conn);
            SqlDataReader reader = commandGenero.ExecuteReader();
            if (!reader.Read()) return Ok("Não existe genero cadastrado!");
            conn.Close();

            conn.Open();
            var query = @"update livro set titulo = @titulo, capa = @capa, sinopse = @sinopse, Preco = @price where id = @id";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id", id);

            command.Parameters.AddWithValue("@titulo", livro.Titulo);
            command.Parameters.AddWithValue("@capa", livro.Capa);
            command.Parameters.Add(new SqlParameter("@sinopse", livro.Sinopse));
            command.Parameters.Add(new SqlParameter("@price", livro.Preco));

            command.ExecuteNonQuery();

            conn.Close();

            return Ok("Livro alterado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar (int id, [FromBody] LivroModel livro)
        {
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var queryGenero = "select * from genero";
            SqlCommand commandGenero = new SqlCommand(queryGenero, conn);
            SqlDataReader reader = commandGenero.ExecuteReader();
            if (!reader.Read()) return Ok("Não existe genero cadastrado!");
            conn.Close();

            conn.Open();
            var query = @"delete from livro where id = @id";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id", id);
            
            command.ExecuteNonQuery();

            conn.Close();

            return Ok("Livro deletado!");
        }
    }
}
