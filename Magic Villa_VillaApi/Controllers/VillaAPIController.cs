using Magic_Villa_VillaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new Villa[] { new Villa() { ID = 1, Name = "Pool Villa" }, new Villa() { ID = 1, Name = "Mountain Villa" } };   
        }
    }
}
