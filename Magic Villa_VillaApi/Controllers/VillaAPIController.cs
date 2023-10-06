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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas() => Ok(VillaStore.villaList);


        //[ProducesResponseType(200)]/*, Type = typeof(VillaDTO))*/
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(v => v.ID == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villa)
        {
            if(villa == null)
            {
                return BadRequest(villa);
            }

            if(villa.ID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villa.ID = VillaStore.villaList.OrderByDescending(u => u.ID).FirstOrDefault().ID + 1;

            if(villa.Name == "string")
            {
                villa.Name = "Test Villa";
            }

            VillaStore.villaList.Add(villa);

            return CreatedAtRoute("GetVilla", new { id = villa.ID }, villa);

        }

    }
}
