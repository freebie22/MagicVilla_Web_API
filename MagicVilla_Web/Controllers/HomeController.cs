using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _service;

        public HomeController(IVillaService service)
        {
            _service = service;
        }
        [HttpGet(Name = "Index")]
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new List<VillaDTO>();

            var response = await _service.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}