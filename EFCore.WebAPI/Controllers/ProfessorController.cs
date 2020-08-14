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
        private readonly IRepository _repository;
        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repository.GetAllProfessores(false);
            if (prof == null) return BadRequest("Não há professores cadastrados na base de dados.");
            return Ok(prof);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _repository.GetProfessor(false);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            return Ok(prof);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            var prof = professor;
            if (prof == null) return BadRequest("Não é possível gravar um professor NULO.");
            _repository.Add(prof);
            if (_repository.SaveChanges())
            {
                return Ok(prof);
            }
            else
            {
                return BadRequest("Falha ao gravar Professor.");
            }
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professor)
        {
            var prof = _repository.GetProfessor(false);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            _repository.Add(prof);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }
            else
            {
                return BadRequest("Falha ao alterar professor.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repository.GetProfessor(false);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            _repository.Add(prof);
            if (_repository.SaveChanges())
            {
                return Ok("Professor removido com sucesso!");
            }
            else
            {
                return BadRequest("Falha ao remover professor.");
            }
        }
    }
}
