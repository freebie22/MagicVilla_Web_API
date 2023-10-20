using Magic_Villa_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa_VillaApi.Data
{
    /// <summary>
    /// Database context of MagicVilla_API applcation
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
              
        }

        /// <summary>
        /// Villas Table in MagicVilla_API Database
        /// </summary>
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 1,
                Name = "Royal Villa",
                Details = "Some default details",
                ImageUrl = "",
                Occupancy = 5,
                Rate = 200,
                Sqft = 550,
                Amenity = "",
                CreatedDate = DateTime.Now,
            },
            new Villa()
            {
                Id = 2,
                Name = "Diamond Villa",
                Details = "Some default diamond villa details",
                ImageUrl = "",
                Occupancy = 6,
                Rate = 300,
                Sqft = 650,
                Amenity = "",
                CreatedDate = DateTime.Now,
            },
            new Villa()
            {
                Id = 3,
                Name = "Ukrainian Villa",
                Details = "Some default ukrainian villa details",
                ImageUrl = "",
                Occupancy = 7,
                Rate = 400,
                Sqft = 750,
                Amenity = "",
                CreatedDate = DateTime.Now,
            }
            );
        }
    }
}
