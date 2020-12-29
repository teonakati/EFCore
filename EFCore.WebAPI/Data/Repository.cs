using EFCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly EntityContext _context;

        public Repository(EntityContext context)
        {
            _context = context;
        }
        public void Add<T>(T any) where T : class
        {
            _context.Add(any);
        }
        public void Update<T>(T any) where T : class
        {
            _context.Update(any);
        }

        public void Remove<T>(T any) where T : class
        {
            _context.Remove(any);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public Aluno[] GetAllAlunos(bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                    .ThenInclude(aluno => aluno.Disciplina)
                    .ThenInclude(aluno => aluno.Professor);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                    .ThenInclude(aluno => aluno.Disciplina)
                    .ThenInclude(aluno => aluno.Professor);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id)
                .Where(aluno => aluno.AlunosDisciplinas.Any(aluno => aluno.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAluno(int alunoId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                    .ThenInclude(aluno => aluno.Disciplina)
                    .ThenInclude(aluno => aluno.Professor);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id)
                .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool incluirAlunos)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(professor => professor.Disciplinas)
                    .ThenInclude(professor => professor.AlunosDisciplinas)
                    .ThenInclude(professor => professor.Aluno);
            }

            query = query.AsNoTracking().OrderBy(professor => professor.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(professor => professor.Disciplinas)
                    .ThenInclude(professor => professor.AlunosDisciplinas)
                    .ThenInclude(professor => professor.Aluno);
            }

            query = query.AsNoTracking().OrderBy(professor => professor.Id)
                .Where(professor => professor.Disciplinas.Any(
                    professor => professor.AlunosDisciplinas.Any(professor => professor.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }

        public Professor GetProfessor(int professorId, bool incluirAlunos)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(professor => professor.Disciplinas)
                    .ThenInclude(professor => professor.AlunosDisciplinas)
                    .ThenInclude(professor => professor.Aluno);
            }

            query = query.AsNoTracking().OrderBy(professor => professor.Id)
                .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}

