using Magic_Villa_VillaApi.Models.DTO;

namespace Magic_Villa_VillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList= new List<VillaDTO>()
        {
            new VillaDTO() {ID = 1, Name = "Pool Villa", Occupancy = 5, SqFt = 100},
            new VillaDTO() {ID = 2, Name = "Mountain Villa", Occupancy = 4, SqFt = 80}
        };
    }
}
