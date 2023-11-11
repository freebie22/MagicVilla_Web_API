using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string VillaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO createDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.POST,
                Data = createDTO,
                Url = VillaUrl + "/api/villaNumberAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.DELETE,
                Url = VillaUrl + $"/api/villaNumberAPI/{id}"
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.GET,
                Url = VillaUrl + $"/api/villaNumberAPI/"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.GET,
                Url = VillaUrl + $"/api/villaNumberAPI/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO updateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.PUT,
                Data = updateDTO,
                Url = VillaUrl + $"/api/villaNumberAPI/{updateDTO.VillaNo}"
            });
        }
    }
}
