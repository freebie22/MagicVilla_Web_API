using Magic_Villa_VillaApi.Models;
using System.Linq.Expressions;

namespace Magic_Villa_VillaApi.Repository
{
    public interface IRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, int pageSize = 0, int pageNumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool isTracked = true, string includeProperties = null);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
