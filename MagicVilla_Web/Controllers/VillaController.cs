using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _service;
        private readonly IMapper _mapper;

        public VillaController(IVillaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet(Name = "Index")]
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new List<VillaDTO>();

            var response = await _service.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateVilla()
        {
            return View("CreateVilla"); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO villaCreateDTO)
        {
            var response = await _service.CreateAsync<APIResponse>(villaCreateDTO);
            if(response != null && response.IsSuccess == true)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(villaCreateDTO);
            
        }

    }
}
