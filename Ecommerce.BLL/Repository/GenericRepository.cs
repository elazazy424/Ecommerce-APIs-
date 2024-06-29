using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Specifications;
using Ecommerce.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext Context;

        public GenericRepository(ApplicationDbContext Context)
        {
            this.Context = Context;
        }

        

        public async Task<IReadOnlyList<T>> GetAllAsync()
         => await Context.Set<T>().AsNoTracking().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // we will use the specification evaluator to get the query and then we will return the result
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), spec);
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
