using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa_VillaApi.Repository
{
    public sealed class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public VillaRepository(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(context)
        {
            _context = context;
            _dbContextFactory = dbContextFactory;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            using(var context = await _dbContextFactory.CreateDbContextAsync())
            {
                entity.UpdateDate = DateTime.Now;
                context.Villas.Update(entity);
                await context.SaveChangesAsync();
            }

        }
    }
}
