using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_VillaApi.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
