using System.Linq.Expressions;
namespace Ecommerce.BLL.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        // List of Includes to be used in the query that generic repository will use
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T,object>> OrderByDescending { get; }

        // Pagination
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
