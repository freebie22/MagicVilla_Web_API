using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string VillaUrl;
        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO createDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.POST,
                Data = createDTO,
                Url = VillaUrl + "/api/villaAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.DELETE,
                Url = VillaUrl + $"/api/villaAPI/{id}"
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.GET,
                Url = VillaUrl + $"/api/villaAPI/"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.GET,
                Url = VillaUrl + $"/api/villaAPI/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO updateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.PUT,
                Data = updateDTO,
                Url = VillaUrl + $"/api/villaAPI/{updateDTO.Id}"
            });
        }
    }
}
