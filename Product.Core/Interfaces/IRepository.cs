using Product.Core.Entities;

namespace Product.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(Guid id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(Guid id);
    }
}
