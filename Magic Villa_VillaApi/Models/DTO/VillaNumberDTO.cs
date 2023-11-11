using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_VillaApi.Models.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
    }
}
