using Client.Exceptions;
using Client.Models.DTO.IdentityDTO;
using Clinet.Models.API;
using Clinet.Services;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly string APIUrl;
        public AccountService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(_clientFactory)
        {
            APIUrl = configuration.GetValue<string>("ServiceUrls:BookLibrary");
        }
        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginDto,
                Url = APIUrl + "/api/Account/Login"
            });

            CheckAPIResponse(apiResponse);

            return Convert.ToString(apiResponse.Result)!;
        }

        public async  Task<string> LoginGoogleAsync(UserDTO result)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = result,
                Url = APIUrl + "/api/Account/LoginGoogle"
            });

            return Convert.ToString(apiResponse.Result)!;
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registerDTO,
                Url = APIUrl + "/api/Account/Register"
            });

            CheckAPIResponse(apiResponse);
        }
        private static void CheckAPIResponse(APIResponse apiResponse)
        {
            if (apiResponse.IsSuccess == false)
            {
                throw new APIException(apiResponse.ErrorMessages.FirstOrDefault());
            }
        }
    }
}
