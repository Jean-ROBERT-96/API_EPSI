using DAL.Filters;
using Entities;

namespace DAL
{
    public interface IRepository<T> where T : IEntity
    {
        public Task<T?> Get(int id);
        public Task<IEnumerable<T>> Get(IFilter filter);
        public Task<T?> Post(T entity);
        public Task<T?> Put(T entity);
        public Task<T?> Delete(int id);
    }
}
