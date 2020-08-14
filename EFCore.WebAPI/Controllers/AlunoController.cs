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
        private readonly IRepository _repository;

        public AlunoController(EntityContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
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
            if (alu == null) return BadRequest("Não é possível gravar um aluno NULO.");
            _repository.Add(alu);
            if (_repository.SaveChanges())
            {
                return Ok(alu);
            }
            else
            {
                return BadRequest("Falha ao gravar aluno.");
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");
            _repository.Update(alu);
            if (_repository.SaveChanges())
            {
                return Ok(alu);
            }
            else
            {
                return BadRequest("Falha ao gravar alterações.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _context.Alunos.FirstOrDefault(x => x.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");
            _repository.Remove(alu);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno removido com sucesso!");
            }
            else
            {
                return BadRequest("Falha ao tentar remover aluno.");
            }
        }
    }
}
