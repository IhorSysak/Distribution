using System.Data.Entity;
using Distributor.DAL.Interfaces;
using Distributor.DAL.EF;

namespace Distributor.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext context;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Delete(int id)
        {
            var item = context.Set<T>().Find(id);
            if (item != null)
            {
                context.Set<T>().Remove(item);
            }
        }

        public IDbSet<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetTByid(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}