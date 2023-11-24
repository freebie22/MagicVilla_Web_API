using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Models.ViewModels;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _service;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService service, IVillaService villaService, IMapper mapper)
        {
            _service = service;
            _villaService = villaService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _service.GetAllAsync<APIResponse>();

            if(response != null && response.IsSuccess)
            {
                var villaNumberList = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
                return View(villaNumberList);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            VillaNumberCreateVM villa = new();

            var response = await _villaService.GetAllAsync<APIResponse>();

            if(response != null && response.IsSuccess)
            {
                villa.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select( i=> new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }

            return View(villa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VillaNumberCreateVM createVM)
        {
            var response = await _service.CreateAsync<APIResponse>(createVM.VillaNumber);

            if(response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else
            {
                if(response.Errors.Count > 0)
                {
                    foreach(var error in response.Errors)
                    {
                        ModelState.AddModelError("Errors accured", error);
                    }
                    return View(createVM);
                }
            }

            return View(createVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return View(nameof(Index));
            }

            var response = await _service.GetAsync<APIResponse>(id);

            if (response != null && response.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaNumberUpdateDTO>(villa));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VillaNumberDTO villaDTO)
        {
            if (villaDTO != null)
            {
                var response = await _service.DeleteAsync<APIResponse>(villaDTO.VillaNo);

                if (response != null && response.IsSuccess)
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

            var villaNumberUpdateVM = new VillaNumberUpdateVM();

            var responseToUpdate = await _service.GetAsync<APIResponse>(id);

            var villaList = await _villaService.GetAllAsync<APIResponse>();

            if (responseToUpdate != null && responseToUpdate.IsSuccess)
            {
                villaNumberUpdateVM.VillaNumber = JsonConvert.DeserializeObject<VillaNumberUpdateDTO>(Convert.ToString(responseToUpdate.Result));
                villaNumberUpdateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(villaList.Result)).Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                    Selected = i.Id == villaNumberUpdateVM.VillaNumber.VillaId
                });

                return View(villaNumberUpdateVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VillaNumberUpdateDTO villaDTO)
        {
            if (villaDTO != null)
            {
                var response = await _service.UpdateAsync<APIResponse>(villaDTO);

                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(villaDTO);
        }
    }
}
