using AutoMapper;
using MagicVilla_Utility;
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
            //string text = System.IO.File.ReadAllText(@"C:\Users\ArtemBoy\Desktop\C# Study\Magic Villa\freebie22\MagicVilla_Web_API\MagicVilla_Web\Controllers\Villas.json");
            //var jsonVillas = JsonConvert.DeserializeObject<List<VillaDTO>>(text);
            //return View(jsonVillas);
            List<VillaDTO> list = new List<VillaDTO>();

            var response = await _service.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
                //string jsonText = JsonConvert.SerializeObject(list, Formatting.Indented);
                //System.IO.File.WriteAllLines(@"C:\Users\ArtemBoy\Desktop\C# Study\Magic Villa\freebie22\MagicVilla_Web_API\MagicVilla_Web\Controllers\Villas.json", new [] {jsonText});
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
            var response = await _service.CreateAsync<APIResponse>(villaCreateDTO, HttpContext.Session.GetString(SD.SessionToken));
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

            var response = await _service.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));

            if(response != null && response.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaUpdateDTO>(villa));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VillaDTO villaDTO)
        {
            if(villaDTO != null)
            {
                var response = await _service.DeleteAsync<APIResponse>(villaDTO.Id, HttpContext.Session.GetString(SD.SessionToken));

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

            var responseToUpdate = await _service.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));

            if(responseToUpdate != null && responseToUpdate.IsSuccess)
            {
                VillaDTO villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(responseToUpdate.Result));
                return View(_mapper.Map<VillaUpdateDTO>(villaDTO));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(VillaUpdateDTO villaDTO)
        {
            if (villaDTO != null)
            {
                var response = await _service.UpdateAsync<APIResponse>(villaDTO, HttpContext.Session.GetString(SD.SessionToken));

                if(response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(villaDTO);
        }
    }
}
