using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeToff.Entities;
using BeToff.BLL;
using BeToff.BLL.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Reflection.Metadata.Ecma335;

namespace BeToff.Web.Controllers
{
    public class UserController : Controller
    {
        protected readonly IUserBc _userBc;
        public UserController(IUserBc userBc)
        {
            _userBc = userBc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Password)
        {

            var credentials = new Credentials(
                Email,
                Password
            );
            User user = await _userBc.ComparePassword(credentials);
            ViewData["ErrorMessage"] = "";

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ViewData["ErrorMessage"] = "Email or password is incorrect";
                return View();
            }
            else
            {
                var Subject = new ClaimsIdentity(new[]
                {
                   new Claim("Email", user.Email),
                   new Claim("UserId", user.Id.ToString())
                },
                CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    //Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(5),


                    IsPersistent = true,


                    IssuedUtc = DateTime.UtcNow
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(Subject),
                    authProperties
                );
                return RedirectToAction(nameof(Index));
            };
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            
            try
            {
                user.IsActive = true;
                user.DateJoined = DateOnly.FromDateTime(DateTime.Today);
                await _userBc.HashPasswordAndInsertUser(user);
                return RedirectToAction(nameof(Login));
            }
            catch (Exception) 
            {
                return View(user);
            }
        }   
    }
}
