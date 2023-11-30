using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient{ get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new APIResponse();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest request)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                if(request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json" );
                }
                switch(request.APIType)
                {
                    case SD.ApiTypes.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiTypes.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiTypes.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage response = null;

                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                response = await client.SendAsync(message);

                var apiContent = await response.Content.ReadAsStringAsync();

                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                try
                {
                    if(apiResponse != null && (apiResponse.StatusCode == HttpStatusCode.BadRequest || apiResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                        throw new BadHttpRequestException("Client Error: Something went wrong with validation of your data.", StatusCodes.Status400BadRequest);
                    }
                }

                catch(BadHttpRequestException ex)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.IsSuccess = false;
                    apiResponse.Errors.Add(ex.Message);
                    var res = JsonConvert.SerializeObject(apiResponse);
                    var returnObj = JsonConvert.DeserializeObject<T>(res);
                    return returnObj;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);

                return APIResponse;
            }

            catch(Exception ex) 
            {
                var dto = new APIResponse()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.Message.ToString() }
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
