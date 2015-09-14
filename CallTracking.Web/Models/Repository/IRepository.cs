using System;
using System.Collections.Generic;

namespace CallTracking.Web.Models.Repository
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        T FirstOrDefault(Func<T, bool> predicate);
        T Find(int id);
        IEnumerable<T> All();
    }
}