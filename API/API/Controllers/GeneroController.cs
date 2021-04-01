using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("genero")]
    public class GeneroController : ControllerBase
    {
        private const string stringConnection = @"Server=54.39.68.137,1433;Database=BancoMarvel;User=herick;Password=Oloco*123;";

        public GeneroController()
        {

        }
        
        [HttpPost]
        public IActionResult AddGenero([FromBody] GeneroModel genero)
        {
            using var conn = new SqlConnection(stringConnection);

            conn.Open();
            var queryGenero = "select * from genero where nome = @nome";

            SqlCommand commandGenero = new SqlCommand(queryGenero, conn);
            commandGenero.Parameters.AddWithValue("@nome", genero.Nome);

            SqlDataReader reader = commandGenero.ExecuteReader();
            if (reader.Read()) return Ok("Gênero já cadastrado!");
            conn.Close();

            conn.Open();
            var query = @"insert into genero (nome) values (@nome)";
            SqlCommand command = new SqlCommand(query, conn);
             
            command.Parameters.AddWithValue("@nome", genero.Nome);

            command.ExecuteNonQuery();

            conn.Close();

            return Ok("Gênero adicionado!");
        }

        [HttpGet]
        public IActionResult ObterGeneros()
        {
            var generoModel = new List<GeneroModel>();
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var query = @"select id,nome from genero";
            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                generoModel.Add(new GeneroModel((int)reader[0], reader[1].ToString()));
            }
            conn.Close();

            return Ok(generoModel);
        }
    }
}
