using EFCore.WebAPI.Data;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EFCore.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly EntityContext _context;

        public AlunoController(EntityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        
        [HttpGet("{id}")]
        public Aluno GetById(int id)
        {
            return _context.Alunos.FirstOrDefault(x => x.Id == id);
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
            if (_context.Alunos.FirstOrDefault(x => x.Id == id) == null) return BadRequest("Nao achou!");

            return Ok("Request Delete do " + _context.Alunos.FirstOrDefault(x => x.Id == id).Nome);
        }
    }
}
