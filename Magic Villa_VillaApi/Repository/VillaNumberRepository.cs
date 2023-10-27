using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Magic_Villa_VillaApi.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateAsync(VillaNumber villaNumber)
        {
            villaNumber.UpdatedDate = DateTime.Now;
            _context.Update(villaNumber);
            await SaveAsync();
        }
    }
}
