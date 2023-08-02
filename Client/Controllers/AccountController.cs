using Clinet.Models.API;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Client.Models.DTO.IdentityDTO;
using System.IdentityModel.Tokens.Jwt;
using Web.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService authService;

        public AccountController(IAccountService _authService)
        {
            authService = _authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginDTO obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                UserDTO model = await authService.LoginAsync(loginDTO);

                var principal = ClaimsIdentity(model);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString(SD.SessionToken, model.Token);

                return RedirectToAction("Index", "Book");

            }

            return View(loginDTO);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO obj)
        {
            if (ModelState.IsValid)
            {
                await authService.RegisterAsync(obj);

                return RedirectToAction("Login");
            }

            return View(obj);

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            SignOut("Cookies", "oidc");
            HttpContext.Session.SetString(SD.SessionToken, "");
            ViewBag.Name = "";
            return RedirectToAction("Index", "Book");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = "https://localhost:7147/api/Account/LoginGoogle",
                Items =
                {
                    { "scheme", CookieAuthenticationDefaults.AuthenticationScheme }
                }
            };

            return Challenge(properties, CookieAuthenticationDefaults.AuthenticationScheme); 

            //await HttpContext.ChallengeAsync(
            //   GoogleDefaults.AuthenticationScheme,
            //   new AuthenticationProperties { RedirectUri = "https://localhost:7147/api/Account/LoginGoogle" });
        }

        //public async Task<IActionResult> GoogleLoginCallback()
        //{
        //    var authenticateResult = await HttpContext.AuthenticateAsync();

        //    if (!authenticateResult.Succeeded)
        //        return View("Login");

        //    var Email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
        //    var DispayName = authenticateResult.Principal.FindFirstValue(ClaimTypes.GivenName);

        //    UserDTO user = new UserDTO()
        //    {
        //        Email = Email,
        //        DisplayName = DispayName,
        //        Token = ""
        //    };

        //    UserDTO model = await authService.LoginGoogleAsync(user);

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal);
        //    HttpContext.Session.SetString(SD.SessionToken, model.Token);

        //    return RedirectToAction("Index", "Book");
        //    // Process the authentication result and sign in the user if successful

        //    // Redirect to a different page or perform additional actions

        //    return RedirectToAction("Index", "Home");
        //}
        private ClaimsPrincipal ClaimsIdentity(UserDTO model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Name = jwt.Claims.FirstOrDefault(u => u.Type == "given_name").Value;
            identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == "email").Value));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, jwt.Claims.FirstOrDefault(u => u.Type == "given_name").Value));

            return new ClaimsPrincipal(identity);
        }
    }
}
