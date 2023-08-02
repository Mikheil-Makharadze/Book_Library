using Client.Models.DTO.IdentityDTO;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDTO> LoginAsync(LoginDTO loginDto);
        Task<UserDTO> LoginGoogleAsync(UserDTO result);
        Task RegisterAsync(RegisterDTO registerDTO);
    }
}
