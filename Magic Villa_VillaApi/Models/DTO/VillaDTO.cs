using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_VillaApi.Models.DTO
{
    public class VillaDTO
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public int SqFt { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

    }
}
