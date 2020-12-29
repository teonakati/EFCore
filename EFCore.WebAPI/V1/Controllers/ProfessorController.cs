using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCore.WebAPI.Data;
using EFCore.WebAPI.V1.Dtos;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.V1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Método que obtém todos Professores cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repository.GetAllProfessores(false);
            if (prof == null) return BadRequest("Não há professores cadastrados na base de dados.");
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(prof));
        }

        /// <summary>
        /// Método que retorna único Professor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _repository.GetProfessor(id, false);
            if (prof == null) return BadRequest("Professor não encontrado na base de dados.");
            return Ok(_mapper.Map<Professor>(prof));
        }

        /// <summary>
        /// Gravar um cadastro de Professor na base de dados
        /// </summary>
        /// <param name="professor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Alterar um cadastro já existente de um Professor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="professor"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professor)
        {
            var prof = _repository.GetProfessor(id, false);
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

        /// <summary>
        /// Excluir um registro de Professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repository.GetProfessor(id, false);
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
