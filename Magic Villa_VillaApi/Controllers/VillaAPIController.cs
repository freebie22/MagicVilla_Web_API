using AutoMapper;
using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Logging;
using Magic_Villa_VillaApi.Mapper;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Magic_Villa_VillaApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Magic_Villa_VillaApi.Controllers
{
    /// <summary>
    /// API Controller for MagicVilla Project
    /// </summary>
    [ApiController]
    [Route("api/VillaAPI")]
    // [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging<VillaAPIController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IVillaRepository _repository;
        protected APIResponse _response;

        public VillaAPIController(ILogging<VillaAPIController> logger, ApplicationDbContext context, IMapper mapper, IVillaRepository repository)
        {
            _logger = logger;    
            _context = context;
            _mapper = mapper;
            _repository = repository;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            //_logger.Log(message:"Succefully got villas from Database", type:LoggingTypes.Info);
            //return Ok(await _context.Villas.ToListAsync());
            var villas = await _repository.GetAllAsync();
            _response.Result = villas;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        } 


        //[ProducesResponseType(200)]/*, Type = typeof(VillaDTO))*/
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log($"Id of villa cannot be {id}", LoggingTypes.Error);
                    return BadRequest();
                }

                var villa = await _repository.GetAsync(v => v.Id == id);

                if (villa == null)
                {
                    //_logger.LogError($"ERROR - Villa with such an id cannot be found.");
                    throw new ArgumentNullException(nameof(id), "Villa with such an id cannot be found.");
                }

                //_logger.LogInformation($"SUCCESS - Succefully got {villa.Name} from VillaStore.");
                _logger.Log($"Villa with id = {id} has been successfully retrived from Database.", LoggingTypes.Info);
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(ArgumentNullException ex)
            {
                _response.Errors = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    throw new ArgumentNullException(nameof(createDTO), "Error with model for create a new Villa");
                }

                if (createDTO.Name == "string")
                {
                    createDTO.Name = "Test Villa";
                    //_logger.LogWarning("Warning - Villa's name has been set as default.");
                }

                Villa model = _mapper.Map<Villa>(createDTO);
                model.CreatedDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;


                await _repository.CreateAsync(entity: model);
                await _repository.SaveAsync();

                _logger.Log($"{model.Name} has been successfully created and added to Database.", LoggingTypes.Info);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;

                return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
            }
            catch(ArgumentNullException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.Message };
                throw;
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    ModelState.AddModelError("ID Error", "ID of villa cannot be 0");
                    return BadRequest(ModelState);
                }

                var villa = await _repository.GetAsync(v => v.Id == id);

                if (villa == null)
                {
                    ModelState.AddModelError("Villa Error", $"Villa with ID {id} hasn't been found.");
                    return NotFound(ModelState);
                }

                await _repository.DeleteAsync(villa);
                await _repository.SaveAsync();

                _logger.Log($"{villa.Name} has been successfully removed from Database.", LoggingTypes.Warning);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = $"${villa.Name} has been successfully removed from Database";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors = new List<string>() { ex.Message };
                throw;
            }
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
        {
            try
            { 
            if (id == 0)
            {
                ModelState.AddModelError("ID Error", "ID of villa cannot be 0");
                throw new ArgumentException("Villa cannot have an id = 0.", nameof(id));
            }

            if(_repository.GetAsync(v => v.Id == id, false) == null)
            {
                throw new ArgumentNullException(nameof(id), $"Villa with id = {id} cannot be found in Database.");
            }

            if(id != villaDTO.Id)
                {
                    throw new ArgumentException("Id of request and VillaId don't match.", nameof(id));
                }

            Villa model = _mapper.Map<Villa>(villaDTO);
            model.UpdateDate = DateTime.Now;

            await _repository.UpdateAsync(model);
            await _repository.SaveAsync();

            _logger.Log($"{model.Name} has been successfully updated.", LoggingTypes.Info);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map(model, typeof(Villa), typeof(VillaDTO));

            return Ok(_response);
            }


            catch(ArgumentNullException ex)
            {
                _response.Errors = new() { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            catch (ArgumentException ex)
            {
                _response.Errors = new List<string>() { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            catch (Exception ex)
            {
               _response.Errors = new List<string>() { ex.Message };
                throw;
            }

        }

        [HttpPatch("{id:int}", Name = "UpdateVillaPartially")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVillaPartially(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            try
            { 
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }

            var villaFromStore = await _repository.GetAsync(v => v.Id == id, false);

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

            await _repository.UpdateAsync(updatedVilla);
            await _repository.SaveAsync();

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<VillaDTO>(updatedVilla);

            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors = new List<string>() { ex.Message };
                throw;
            }
        }



    }
}
