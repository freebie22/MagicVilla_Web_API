using Magic_Villa_VillaApi.Models.DTO;

namespace Magic_Villa_VillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList= new List<VillaDTO>()
        {
            new VillaDTO() {ID = 1, Name = "Pool Villa"},
            new VillaDTO() {ID = 2, Name = "Mountain Villa"}
        };
    }
}
