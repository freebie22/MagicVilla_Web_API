using Magic_Villa_VillaApi.Models;
using System.Linq.Expressions;

namespace Magic_Villa_VillaApi.Repository
{
    /// <summary>
    /// Repository that gives a functionality related to Villa Model.
    /// </summary>
    public interface IVillaRepository : IRepository<Villa>
    {
        Task UpdateAsync(Villa entity);
    }
}
