using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.WebAPI.Data;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly EntityContext _context;

        public ProfessorController(EntityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var prof = _context.Professores;
            if (prof == null) return BadRequest("Não há professores cadastrados na base de dados.");
            return Ok(prof);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _context.Professores.FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            return Ok(prof);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            var prof = professor;
            if (prof == null) return BadRequest("Ocorreu um erro ao gravar professor.");
            _context.Add(prof);
            _context.SaveChanges();
            return Ok(prof);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _context.Professores.FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            _context.Remove(prof);
            _context.SaveChanges();
            return Ok("Professor removido com sucesso!");
        }
    }
}
