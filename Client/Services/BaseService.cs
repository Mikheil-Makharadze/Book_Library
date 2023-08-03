using Clinet.Models.API;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Clinet.Services
{
    public class BaseService
    {
        public IHttpClientFactory HttpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.HttpClient = httpClient;
        }
        public async Task<APIResponse> SendAsync(APIRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("BookLibrary");

                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                message.Method = apiRequest.ApiType switch
                {
                    SD.ApiType.GET => HttpMethod.Get,
                    SD.ApiType.POST => HttpMethod.Post,
                    SD.ApiType.PUT => HttpMethod.Put,
                    SD.ApiType.DELETE => HttpMethod.Delete,
                    _ => throw new Exception("Unknown Http Method"),
                };
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                var apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<APIResponse>(apiContent)!;

            }
            catch (Exception ex)
            {

                return new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
            }
        }
    }
}
