using Clinet.Models.API;

namespace Clinet.Services.Interfaces
{
    public interface IBaseService
    {
        Task<APIResponse> SendAsync(APIRequest apiRequest);
    }
}
