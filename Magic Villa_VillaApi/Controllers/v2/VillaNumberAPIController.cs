using AutoMapper;
using Azure;
using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Magic_Villa_VillaApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Magic_Villa_VillaApi.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/VillaNumberAPI")]
    [ApiVersion("2.0")]
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

        [HttpGet("getStrings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> GetString()
        {
            _response.Result = new[] { "Artem", "Boikov" };
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

    }
}
