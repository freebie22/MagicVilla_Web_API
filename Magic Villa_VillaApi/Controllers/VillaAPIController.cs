using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Logging;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
   // [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {

        private readonly ILogging _logger;

        public VillaAPIController(ILogging _logger)
        {
            this._logger = _logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas", "SUCCESS");
            return Ok(VillaStore.villaList); 
        }
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
                //_logger.LogError($"Get Villa Error with ID = {id}");
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(v => v.ID == id);

            if (villa == null)
            {
                _logger.Log("Villa hasn't been found.", "ERROR");
                return NotFound();
            }

            //_logger.LogInformation($"Got villa with such parameters: \n" +
            //    $"Villa's name = {villa.Name}\n" +
            //    $"Villa's occupancy = {villa.Occupancy}\n" +
            //    $"Villa's square = {villa.SqFt} sqft");
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
                //_logger.LogError("Villa cannot be created");
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
                //_logger.LogWarning("The name of new villa has set to Test Villa, because it was by default.");
            }

            VillaStore.villaList.Add(villa);


            //_logger.LogInformation($"New villa succesfully created. You can see result in new callback of GET method.");
            return CreatedAtRoute("GetVilla", new { id = villa.ID }, villa);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                ModelState.AddModelError("Zero ID Error", "Id of villa cannot be 0");
                return BadRequest();
            }

            var villaFromStore = VillaStore.villaList.FirstOrDefault(v => v.ID == id);

            if(villaFromStore == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villaFromStore);

            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> UpdateVilla(int id, [FromBody] VillaDTO villa)
        {
            if (id == 0)
            {
                ModelState.AddModelError("Zero ID Error", "Id of villa cannot be 0");
                return BadRequest();
            }

            var villaFromStore = VillaStore.villaList.FirstOrDefault(v => v.ID == id);

            if (villaFromStore == null)
            {
                return NotFound();
            }

            villaFromStore.Name = villa.Name;
            villaFromStore.Occupancy = villa.Occupancy;
            villaFromStore.SqFt = villa.SqFt;

            return Ok(villaFromStore);
        }
        [HttpPatch("{id:int}", Name = "UpdateVillaPartially")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVillaPartially(int id, JsonPatchDocument<VillaDTO> jsonDTO)
        {
            if (id == 0)
            {
                ModelState.AddModelError("Zero ID Error", "Id of villa cannot be 0");
                return BadRequest();
            }

            var villaFromStore = VillaStore.villaList.FirstOrDefault(v => v.ID == id);

            if (villaFromStore == null)
            {
                return NotFound();
            }

            jsonDTO.ApplyTo(villaFromStore, ModelState);

            return NoContent();
        }

    }
}
