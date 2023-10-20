using Magic_Villa_VillaApi.Models.DTO;

namespace Magic_Villa_VillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList= new List<VillaDTO>()
        {
            new VillaDTO() {Id = 1, Name = "Pool Villa", Occupancy = 3, Sqft = 60},
            new VillaDTO() {Id = 2, Name = "Mountain Villa", Occupancy = 4, Sqft = 100}
        };
    }
}
