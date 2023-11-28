using AutoMapper;
using Azure;
using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Magic_Villa_VillaApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Magic_Villa_VillaApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/VillaNumberAPI")]
    [ApiVersion("1.0", Deprecated = true)]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly ApplicationDbContext _context;
        public VillaNumberAPIController(IVillaNumberRepository villaNumberRepository, IMapper mapper, ApplicationDbContext context, IVillaRepository villaRepository)
        {
            _villaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            _context = context;
            _response = new APIResponse();
            _villaRepository = villaRepository;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            var villaNumbers = await _villaNumberRepository.GetAllAsync(includeProperties: "Villa");
            if (villaNumbers.Count == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { "No villa numbers can be found in Database." };
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumbers);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("getStrings")]
        public async Task<ActionResult<APIResponse>> Get()
        {
            IEnumerable<string> strings = new[] { "value1", "value2" };
            _response.Result = strings;
            return Ok(_response);
        }

        [HttpGet("{villaNo:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int villaNo)
        {
            try
            {
                if (villaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    throw new Exception("VillaNo cannot be 0.");
                }
                var villa = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException(nameof(villaNo), "Villa number with such a VillaNo cannot be found.");
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }

            catch (ArgumentNullException ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                return NotFound(_response);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpPost(Name = "CreateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO villaNumberCreateDTO)
        {
            try
            {
                if (villaNumberCreateDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentNullException("The body of POST request is empty. Please, check your input data.", nameof(villaNumberCreateDTO));
                }

                if (await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNumberCreateDTO.VillaNo, false) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentException("Server Error: Sorry, but you have entered an already existing VillaNo. Please, check your input data.", nameof(villaNumberCreateDTO.VillaNo));
                }

                if (await _villaRepository.GetAsync(v => v.Id == villaNumberCreateDTO.VillaId) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException("Sorry, but you have inputed an not-exist Villa Id. Please, check it one more time.", nameof(villaNumberCreateDTO.VillaId));
                }

                var villa = _mapper.Map<VillaNumber>(villaNumberCreateDTO);
                villa.CreatedDate = DateTime.Now;
                villa.UpdatedDate = DateTime.Now;

                await _villaNumberRepository.CreateAsync(villa);
                await _villaNumberRepository.SaveAsync();

                return CreatedAtRoute(nameof(GetVillaNumber), new { villaNo = villa.VillaNo }, villa);

            }


            catch (ArgumentNullException ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                switch (_response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return NotFound(_response);
                    case HttpStatusCode.BadRequest:
                        return BadRequest(_response);
                }
                throw;
            }

            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new() { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{villaNo:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteNumber(int villaNo)
        {
            try
            {
                if (villaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentException($"Provided villaNo {villaNo} cannot be processed", nameof(villaNo));
                }

                var villa = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo);

                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException($"Villa with villaNo {villaNo} cannot be found in Database.", nameof(villaNo));
                }

                await _villaNumberRepository.DeleteAsync(villa);
                await _villaNumberRepository.SaveAsync();

                _response.Result = $"VillaNumber with villaNo {villaNo} has been deleted successfully.";
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpPut("{villaNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateNumber(int villaNo, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (villaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentException($"Provided villaNo {villaNo} cannot be processed", nameof(villaNo));
                }

                if (updateDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentNullException($"Something is empty in your body request. Please, check your input data.", nameof(updateDTO));
                }

                if (await _villaRepository.GetAsync(v => v.Id == updateDTO.VillaId) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentNullException("Sorry, but you have inputed an not-exist Villa Id. Please, check it one more time.", nameof(updateDTO.VillaId));
                }

                if (villaNo != updateDTO.VillaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentException($"Please, check villaNo parameter, because it doesn't match to your body request", nameof(villaNo));
                }

                var villa = _mapper.Map<VillaNumber>(updateDTO);

                await _villaNumberRepository.UpdateAsync(villa);
                await _villaNumberRepository.SaveAsync();

                _response.Result = $"VillaNumber with villaNo {villaNo} has been updated successfully.";
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.Message };
                return NotFound(_response);
            }
        }

        [HttpPatch("{villaNo:int}", Name = "UpdateVillaNumberPartially")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumberPartially(int villaNo, JsonPatchDocument<VillaNumberUpdateDTO> jsonDTO)
        {
            try
            {
                if (villaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentException($"Provided villaNo {villaNo} cannot be processed", nameof(villaNo));
                }

                var villaNumberFromDatabase = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo, false);

                if (villaNumberFromDatabase == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException($"VillaNumber with Provided villaNo {villaNo} cannot be found", nameof(villaNo));
                }

                var villaUpdate = _mapper.Map<VillaNumberUpdateDTO>(villaNumberFromDatabase);

                jsonDTO.ApplyTo(villaUpdate, ModelState);

                if (await _villaRepository.GetAsync(v => v.Id == villaUpdate.VillaId) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    throw new ArgumentNullException("Sorry, but you have inputed an not-exist Villa Id. Please, check it one more time.", nameof(villaUpdate.VillaId));
                }

                var villaToUpdate = _mapper.Map<VillaNumber>(villaUpdate);

                await _villaNumberRepository.UpdateAsync(villaToUpdate);
                await _villaNumberRepository.SaveAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = $"VillaNumber with villaNo {villaNo} has been partially updated successfully.";

                return Ok(_response);

            }
            catch (ArgumentNullException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { ex.Message };
                return NotFound(_response);
            }

            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }
    }
}
