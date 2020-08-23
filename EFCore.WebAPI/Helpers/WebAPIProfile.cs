using AutoMapper;
using EFCore.WebAPI.Dtos;
using EFCore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Helpers
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
        }
    }
}