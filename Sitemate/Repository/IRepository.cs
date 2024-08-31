using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitemate.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id);
    }
}
