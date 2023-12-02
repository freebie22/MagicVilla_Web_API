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
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public VillaNumberRepository(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(context)
        {
            _context = context;
            _dbContextFactory = dbContextFactory;
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VillaNumber villaNumber)
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                villaNumber.UpdatedDate = DateTime.Now;
                context.Update(villaNumber);
                await context.SaveChangesAsync();
            }

        }
    }
}
