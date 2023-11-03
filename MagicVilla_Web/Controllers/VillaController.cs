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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return View(nameof(Index));
            }

            VillaDTO villa = new();

            var response = await _service.GetAsync<APIResponse>(id);

            if(response != null && response.IsSuccess)
            {
                villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
            }

            return View(villa);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VillaDTO villaDTO)
        {
            if(villaDTO != null)
            {
                var response = await _service.DeleteAsync<APIResponse>(villaDTO.Id);

                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(villaDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                return View(nameof(Index));
            }

            var villaDTO = new VillaUpdateDTO();

            var responseToUpdate = await _service.GetAsync<APIResponse>(id);

            if(responseToUpdate != null && responseToUpdate.IsSuccess)
            {
                villaDTO = JsonConvert.DeserializeObject<VillaUpdateDTO>(Convert.ToString(responseToUpdate.Result));
            }

            return View(villaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VillaUpdateDTO villaDTO)
        {
            if (villaDTO != null)
            {
                var response = await _service.UpdateAsync<APIResponse>(villaDTO);

                if(response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(villaDTO);
        }
    }
}
