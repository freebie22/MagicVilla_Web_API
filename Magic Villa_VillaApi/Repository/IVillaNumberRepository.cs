using Magic_Villa_VillaApi.Models;

namespace Magic_Villa_VillaApi.Repository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task UpdateAsync(VillaNumber villaNumber);
        Task SaveAsync();
    }
}
