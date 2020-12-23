using System.Collections.Generic;
using System.Data.Entity;

namespace Distributor.DAL.Interfaces
{
    interface IRepository<T> where T : class
    {
        IDbSet<T> GetAll();
        T GetTByid(int id);
        void Delete(int id);
        void Update(T item);
        void Create(T item);
    }
}
