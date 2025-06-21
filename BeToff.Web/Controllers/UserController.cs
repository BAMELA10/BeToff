using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeToff.Entities;
using BeToff.BLL;
using BeToff.BLL.Interface;

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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception) 
            {
                return View(user);
            }
        }
    }
}
