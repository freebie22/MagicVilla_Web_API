using Magic_Villa_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa_VillaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Villa> Villas { get; set; }
    }
}
