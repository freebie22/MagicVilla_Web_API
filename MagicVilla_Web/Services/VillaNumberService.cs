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

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO createDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.POST,
                Data = createDTO,
                Url = VillaUrl + "/api/v1/villaNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.DELETE,
                Url = VillaUrl + $"/api/v1/villaNumberAPI/{id}",
                Token = token
            });
        }

        public async Task<T> GetAllAsync<T>(string token)
        {
            return await SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.GET,
                Url = VillaUrl + $"/api/v1/villaNumberAPI/",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.GET,
                Url = VillaUrl + $"/api/v1/villaNumberAPI/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO updateDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.ApiTypes.PUT,
                Data = updateDTO,
                Url = VillaUrl + $"/api/v1/villaNumberAPI/{updateDTO.VillaNo}",
                Token = token
            });
        }
    }
}
