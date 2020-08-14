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

    }
}

