using Client.Models.DTO.IdentityDTO;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginDTO loginDto);
        Task<string> LoginGoogleAsync(UserDTO result);
        Task RegisterAsync(RegisterDTO registerDTO);
    }
}
