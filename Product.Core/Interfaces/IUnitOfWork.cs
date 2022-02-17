
using Product.Core.Interfaces;

namespace Product.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {


        void SaveChanges();

        Task SaveChangesAsync();

    }
}
