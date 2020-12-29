using AutoMapper;
using EFCore.WebAPI.V1.Dtos;
using EFCore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.WebAPI.Helpers;

namespace EFCore.WebAPI.V1.Helpers
{
    public class WebAPIProfile : Profile
    {
        public WebAPIProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(alunoDto => alunoDto.Nome, opt => opt.MapFrom(aluno => $"{aluno.Nome} {aluno.Sobrenome}"))
                .ForMember(alunoDto => alunoDto.Idade, opt => opt.MapFrom(aluno => aluno.DataNasc.ObterIdade()));

            CreateMap<AlunoDto, Aluno>();

            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                .ForMember(professorDto => professorDto.Nome, opt => opt.MapFrom(professor => $"{professor.Nome} {professor.Sobrenome}"));

            CreateMap<ProfessorDto, Professor>();
        }
    }
}