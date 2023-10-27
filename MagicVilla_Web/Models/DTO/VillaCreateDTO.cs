using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required, DisplayName("Назва вілли")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Деталі")]
        public string Details { get; set; }
        [Required]
        [DisplayName("Ціна оренди")]
        public double Rate { get; set; }
        [Required]
        [DisplayName("Площа м^2")]
        public int Sqft { get; set; }
        [Required]
        [DisplayName("Вмістимість")]
        public int Occupancy { get; set; }
        [Required]
        [DisplayName("Зображення")]
        public string ImageUrl { get; set; }
        [Required]
        [DisplayName("Зручності")]
        public string Amenity { get; set; }
    }
}
