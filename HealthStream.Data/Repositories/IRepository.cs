using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthStream.Data.Repositories
{
    public interface IRepository<T> where T: class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Delete(int id);
        void Udate(T entity);
        void Insert(T entity);
        void SaveChanges();
    }
}
