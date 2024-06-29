using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Entites;
using System.Collections;

namespace Ecommerce.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context; 
        }
        public Task<int> Complete()
         => _context.SaveChangesAsync();
        

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity<int>
        {
            if (_repositories == null) _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
