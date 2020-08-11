using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EFCore.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>()
        {
            new Aluno()
            {
                Id = 1,
                Nome = "Teo",
                Sobrenome = "Nakati",
                Telefone = "67 981512546"
            },
            new Aluno()
            {
                Id = 2,
                Nome = "Carlos",
                Sobrenome = "Teixeira",
                Telefone = "67 543242133"
            },
            new Aluno()
            {
                Id = 3,
                Nome = "Pedro",
                Sobrenome = "Castro",
                Telefone = "67 231232133"
            },
            new Aluno()
            {
                Id = 4,
                Nome = "Leonardo",
                Sobrenome = "Nakati",
                Telefone = "67 1231231231"
            },

        };

        public AlunoController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public Aluno GetById(int id)
        {
            return Alunos.Find(x => x.Id == id);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
