﻿using AutoMapper;
using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Logging;
using Magic_Villa_VillaApi.Mapper;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
   // [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging<VillaAPIController> _logger;
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;

        public VillaAPIController(ILogging<VillaAPIController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;    
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            //_logger.Log(message:"Succefully got villas from Database", type:LoggingTypes.Info);
            //return Ok(await _context.Villas.ToListAsync());
            var villas = await _context.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villas));
        } 


        //[ProducesResponseType(200)]/*, Type = typeof(VillaDTO))*/
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<VillaDTO>> GetVilla(int id)
        {
            if(id == 0)
            {
                _logger.Log($"Id of villa cannot be {id}", LoggingTypes.Error);
                return BadRequest();
            }

            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                //_logger.LogError($"ERROR - Villa with such an id cannot be found.");
                return NotFound();
            }

            //_logger.LogInformation($"SUCCESS - Succefully got {villa.Name} from VillaStore.");
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Villa>> CreateVilla([FromBody]VillaCreateDTO createDTO)
        {
            if(createDTO == null)
            {
                return BadRequest(createDTO);
            }

            if(createDTO.Name == "string")
            {
                createDTO.Name = "Test Villa";
                //_logger.LogWarning("Warning - Villa's name has been set as default.");
            }

            Villa model = _mapper.Map<Villa>(createDTO);
            model.CreatedDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;

            //Villa model = new Villa()
            //{
            //    Name = createDTO.Name,
            //    Amenity = createDTO.Amenity,
            //    Details = createDTO.Details,
            //    ImageUrl = createDTO.ImageUrl,
            //    CreatedDate = DateTime.Now,
            //    Sqft = createDTO.Sqft,
            //    Occupancy = createDTO.Occupancy,
            //    Rate = createDTO.Rate,
            //    UpdateDate = DateTime.Now
            //};

            await _context.Villas.AddAsync(entity:model);
            await _context.SaveChangesAsync();

            _logger.Log($"{model.Name} has been successfully created and added to Database.", LoggingTypes.Info);

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                ModelState.AddModelError("ID Error", "ID of villa cannot be 0");
                return BadRequest(ModelState);
            }

            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);

            if(villa == null)
            {
                ModelState.AddModelError("Villa Error", $"Villa with ID {id} hasn't been found.");
                return NotFound(ModelState);
            }

            _context.Villas.Remove(villa);
            _context.SaveChanges();

            _logger.Log($"{villa.Name} has been successfully removed from Database.", LoggingTypes.Warning);

            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
        {
            if (id == 0)
            {
                ModelState.AddModelError("ID Error", "ID of villa cannot be 0");
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(villaDTO);
            model.UpdateDate = DateTime.Now;

            //Villa updatedVilla = new Villa()
            //{
            //    Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Sqft = villaDTO.Sqft,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    UpdateDate = DateTime.Now
            //};

            _context.Villas.Update(model);
            _context.SaveChanges();

            _logger.Log($"{model.Name} has been successfully removed from Database.", LoggingTypes.Info);

            return Ok(model);
        }

        [HttpPatch("{id:int}", Name = "UpdateVillaPartially")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVillaPartially(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }

            var villaFromStore = _context.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            if (villaFromStore == null)
            {
                ModelState.AddModelError("Villa Error", $"Villa with ID {id} hasn't been found.");
                return NotFound(ModelState);
            }

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villaFromStore);

            //VillaUpdateDTO villaDTO = new ()
            //{
            //    Id = villaFromStore.Id,
            //    Name = villaFromStore.Name,
            //    Amenity = villaFromStore.Amenity,
            //    Details = villaFromStore.Details,
            //    ImageUrl = villaFromStore.ImageUrl,
            //    Sqft = villaFromStore.Sqft,
            //    Occupancy = villaFromStore.Occupancy,
            //    Rate = villaFromStore.Rate,
            //};

            patchDTO.ApplyTo(villaDTO, ModelState);

            Villa updatedVilla = _mapper.Map<Villa>(villaDTO);

            _context.Villas.Update(updatedVilla);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
