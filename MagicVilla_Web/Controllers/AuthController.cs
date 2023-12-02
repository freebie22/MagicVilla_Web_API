using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _serivce;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, IMapper mapper, ILogger<AuthController> logger)
        {
            _serivce = service;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginDTO = new();
            return View(loginDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            if(ModelState.IsValid)
            {
                APIResponse aPIResponse = await _serivce.LoginAsync<APIResponse>(obj);
                if(aPIResponse != null && aPIResponse.IsSuccess)
                {
                    LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(aPIResponse.Result));

                    var jwtHandler = new JwtSecurityTokenHandler();

                    var jwt = jwtHandler.ReadJwtToken(model.Token);

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(v => v.Type == "unique_name").Value));
                    identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(v => v.Type == "role").Value));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    HttpContext.Session.SetString(SD.SessionToken, model.Token);

                    TempData["success"] = $"You're welcome!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegistrationRequestDTO registerDTO = new();
            return View(registerDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {
            if(ModelState.IsValid)
            {
                APIResponse response =  await _serivce.RegisterAsync<APIResponse>(obj);
                if(response.IsSuccess && response != null)
                {
                    TempData["success"] = "You've been successfully registrated on our website. Please, login to contiue."; 
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["error"] = $"Something wrong with your fields. Please, check them and try to register one more time";
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            TempData["success"] = $"You've been successfully logged out!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
