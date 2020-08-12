using EFCore.WebAPI.Data;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
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
            var alu = _context.Alunos;
            if (alu == null) return BadRequest("Não há alunos cadastrados na base de dados.");
            return Ok(alu);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var alu = _context.Alunos.FirstOrDefault(x => x.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");
            return Ok(alu);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Aluno aluno)
        {
            var alu = aluno;
            if (alu == null) return BadRequest("Ocorreu um erro ao gravar aluno.");
            _context.Add(alu);
            _context.SaveChanges();
            return Ok(alu);
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _context.Alunos.FirstOrDefault(x => x.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");
            _context.Remove(alu);
            _context.SaveChanges();
            return Ok("Aluno removido com sucesso!");
        }
    }
}
