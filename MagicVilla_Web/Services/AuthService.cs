using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services;

namespace MagicVilla_Web.Repository
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClient;
        protected readonly string _villaUrl;    
        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {

            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

        }
        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                Url = _villaUrl + "/api/UsersAuth/Login",
                Data = loginRequestDTO,
                APIType = SD.ApiTypes.POST
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                Url = _villaUrl + "/api/UsersAuth/Register",
                Data = registrationRequestDTO,
                APIType = SD.ApiTypes.POST
            });
        }
    }
}
