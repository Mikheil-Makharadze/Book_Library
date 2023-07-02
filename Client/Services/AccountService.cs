using Client.Models.DTO.IdentityDTO;
using Clinet.Models.API;
using Clinet.Services;
using Newtonsoft.Json;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private string APIUrl;
        public AccountService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(_clientFactory)
        {
            APIUrl = configuration.GetValue<string>("ServiceUrls:BookLibrary");
        }
        public async Task<UserDTO> LoginAsync(LoginDTO loginDto)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginDto,
                Url = APIUrl + "/api/Account/Login"
            });

            return JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(apiResponse.Result));

        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registerDTO,
                Url = APIUrl + "/api/Account/Register"
            });
        }
    }
}
