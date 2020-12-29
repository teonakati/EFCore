using AutoMapper;
using EFCore.WebAPI.Data;
using EFCore.WebAPI.V2.Dtos;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace EFCore.WebAPI.V2.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os Alunos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alu = _repository.GetAllAlunos(true);
            if (alu == null) return BadRequest("Não há alunos cadastrados na base de dados.");

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alu));
        }

        /// <summary>
        /// Retorna um único aluno buscando por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var alu = _repository.GetAluno(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado na base de dados.");
            return Ok(_mapper.Map<Aluno>(alu));
        }

        /// <summary>
        /// Grava um novo cadastro de Aluno na base de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Busca um aluno por ID com uma classe de retorno personalizada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Exclui um registro de Aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
