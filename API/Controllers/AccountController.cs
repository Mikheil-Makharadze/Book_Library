using API.DTO.IdentityDTO;
using API.Response;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.CustomException;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    /// <summary>
    /// Account staff
    /// </summary>
    public class AccountController : BaseApiContoller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;

        /// <summary>
        /// injecting services
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login(LoginDTO loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded)
                {
                    throw new BadRequestException("Email or Password is incorrect");
                }

                return Ok(new APIResponse(tokenService.CreateToken(user)));
            }

            throw new BadRequestException("Email or Password is incorrect");
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Register(RegisterDTO registerDto)
        {
            var email = await userManager.FindByEmailAsync(registerDto.Email);

            if (email != null)
            {
                throw new BadRequestException("Account with this Email already exists");
            }

            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Error while Registering");
            }

            return Ok(new APIResponse());
        }

        //[HttpPost("LoginGoogle")]
        //public async Task<ActionResult<APIResponse>> LoginGoogle(UserDTO result)
        //{
        //    var email = result.Email;

        //    if (email != null)
        //    {
        //        // Create a new user without password if we do not have a user already
        //        var user = await userManager.FindByEmailAsync(email);

        //        if (user == null)
        //        {
        //            user = new User
        //            {
        //                Email = email,
        //                DisplayName = result.DisplayName
        //            };

        //            await userManager.CreateAsync(user);
        //        }
        //        await signInManager.SignInAsync(user, isPersistent: false);
        //        //var info = await signInManager.CheckPasswordSignInAsync(user, user.PasswordHash, false);

        //        var newUser = new UserDTO
        //        {
        //            Email = user.UserName,
        //            DisplayName = user.DisplayName,
        //            Token = await tokenService.CreateToken(user)
        //        };

        //        return Ok(new APIResponse
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            IsSuccess = true,
        //            Result = newUser
        //        });
        //    }

        //    return Ok(new APIResponse
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        IsSuccess = false
        //    });
        //}

    }
}
