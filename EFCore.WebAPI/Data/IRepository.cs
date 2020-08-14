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
    }
}
