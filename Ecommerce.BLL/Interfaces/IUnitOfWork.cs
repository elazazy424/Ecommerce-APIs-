using Ecommerce.DAL.Entites;

namespace Ecommerce.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity<int>;
        Task<int> Complete();

    }
}
