using AutoMapper;
using EFCore.WebAPI.Data;
using EFCore.WebAPI.Dtos;
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
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alu = _repository.GetAllAlunos(true);
            if (alu == null) return BadRequest("Não há alunos cadastrados na base de dados.");

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alu));
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var alu = _repository.GetAluno(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");

            var alunoDto = _mapper.Map<Aluno>(alu);

            return Ok(alunoDto);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] AlunoRegistrarDto model)
        {
            if (model == null) return BadRequest("Não é possível gravar um aluno NULO.");

            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            else
            {
                return BadRequest("Falha ao gravar aluno.");
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AlunoRegistrarDto model)
        {
            var alu = _repository.GetAluno(id, false);

            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");

            _mapper.Map(model, alu);

            _repository.Update(alu);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alu));
            }
            else
            {
                return BadRequest("Falha ao gravar alterações.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repository.GetAluno(id, false);
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
