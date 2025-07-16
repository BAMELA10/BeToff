using BeToff.BLL.Interface;
using BeToff.Entities;
using BeToff.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BeToff.Web.Controllers
{
    public class FamillyController : Controller
    {
        private readonly IFamillyBc _famillyBc;
        private readonly IRegistrationBc _registrationBc;

        public FamillyController(IFamillyBc famillyBc, IRegistrationBc registrationBc)
        {
            _famillyBc = famillyBc;
            _registrationBc = registrationBc;
        }
        public async Task<ActionResult> Index()
        {

            string CurrentUserId = User.FindFirst("UserId").Value;
            //ViewData["UserId"] = CurrentUserId;
            var Data = await _famillyBc.SelectFamillyByHead(CurrentUserId);

            var model = new FamillyViewModel()
            {
                famillies = Data,
            };

            return View(model);
        }



        public ActionResult Details()
        {
            return View();
        }

        public async Task<ActionResult> Details(string id)
        {
            var Items = await _famillyBc.SelectFamilly(id);
            return View(Items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string NameOfFamilly)
        {
            
            try
            {
                string CurrentUser = User.FindFirst("UserId").Value;
                Guid IdFamilly = await _famillyBc.SaveFamilly(NameOfFamilly, CurrentUser);
                await _registrationBc.RegistrationOfMemberOfFamilly(IdFamilly, CurrentUser);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(nameof(Index));
            }
        }

    }
}
