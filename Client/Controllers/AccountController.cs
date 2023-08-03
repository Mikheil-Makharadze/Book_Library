using Clinet.Models.API;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Client.Models.DTO.IdentityDTO;
using System.IdentityModel.Tokens.Jwt;
using Web.Services.Interfaces;
using Client.Exceptions;

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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string model = await authService.LoginAsync(loginDTO);
                    var principal = ClaimsIdentity(model);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString(SD.SessionToken, model);

                    return RedirectToAction("Index", "Book");
                }
                catch(APIException ex)
                {
                    ModelState.AddModelError("", ex.Message);                    
                }
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
                try
                {
                    await authService.RegisterAsync(obj);

                    return RedirectToAction("Login");
                }
                catch (APIException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
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

        //public IActionResult GoogleLogin()
        //{
        //    var properties = new AuthenticationProperties
        //    {
        //        RedirectUri = "https://localhost:7147/api/Account/LoginGoogle",
        //        Items =
        //        {
        //            { "scheme", CookieAuthenticationDefaults.AuthenticationScheme }
        //        }
        //    };

        //    return Challenge(properties, CookieAuthenticationDefaults.AuthenticationScheme); 

        //    //await HttpContext.ChallengeAsync(
        //    //   GoogleDefaults.AuthenticationScheme,
        //    //   new AuthenticationProperties { RedirectUri = "https://localhost:7147/api/Account/LoginGoogle" });
        //}
        private ClaimsPrincipal ClaimsIdentity(string model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Name = jwt.Claims.FirstOrDefault(u => u.Type == "given_name")!.Value;
            identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == "email")!.Value));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, jwt.Claims.FirstOrDefault(u => u.Type == "given_name")!.Value));

            return new ClaimsPrincipal(identity);
        }
    }
}
