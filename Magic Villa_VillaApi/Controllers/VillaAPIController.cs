using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
   // [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.villaList;
        }
        [HttpGet("id:int")]
        public VillaDTO GetVilla(int id)
        {
            return VillaStore.villaList.FirstOrDefault(v => v.ID == id);
        }
    }
}
