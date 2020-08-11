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

        
        [HttpGet("{id}")]
        public Aluno GetById(int id)
        {
            return Alunos.Find(x => x.Id == id);
        }

        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (Alunos.Find(x => x.Id == id) == null) return BadRequest("Nao achou!");

            return Ok("Request Delete do " + Alunos.Find(x => x.Id == id).Nome);
        }
    }
}
