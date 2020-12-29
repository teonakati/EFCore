using EFCore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T any) where T : class;
        void Update<T>(T any) where T : class;
        void Remove<T>(T any) where T : class;
        bool SaveChanges();

        Aluno[] GetAllAlunos(bool incluirProfessor);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor);
        Aluno GetAluno(int alunoId, bool incluirProfessor);
        Professor[] GetAllProfessores(bool incluirAlunos);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos);
        Professor GetProfessor(int professorId, bool incluirAlunos);

    }
}
