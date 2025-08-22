using BeToff.BLL.Interface;
using BeToff.BLL.Service.Interface;
using BeToff.Entities;
using BeToff.Web.Hubs;
using BeToff.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
            var Data = await _registrationBc.ListOfRegistrationForUser(CurrentUserId);

            var model = new RegistrationViewModel()
            {
                Registrations = Data,
            };

            return View(model);
        }



        public ActionResult Album ()
        {
            return View();
        }
        //public async Task<ActionResult> Album (string Id)
        //{
        //    //
        //}
        public ActionResult Details()
        {
            return View();
        }

        public async Task<ActionResult> Details(string id)
        {
            var Items = await _famillyBc.SelectFamilly(id);
            return View(Items);
        }

        [Route("Familly/{Id}/Home")]
        public async Task<ActionResult> Home(string Id)
        {
            Console.WriteLine(Id);
            var FamilyItem = await _famillyBc.SelectFamilly(Id);
            var model = new FamillyViewModel
            {
                Familly = FamilyItem,
            };
            return View(model);
        }

        [Route("Familly/{Id}/Members")]
        public async Task<ActionResult> Members(string Id)
        {
            string CurrentUser = User.FindFirst("UserId")!.Value;
            ViewData["CurrentUser"] = CurrentUser;
            var AllFamillyMember = await _registrationBc.ListOfRegistrationForFamilly(Id);
            var model = new RegistrationViewModel
            {
                Registrations = AllFamillyMember
            };
            //affect that list to his viewmodel for print it
            return View(model);
        }

        //[Route("Familly/{Id}/RemoveMember/{MemberId}")]
        //public async Task<ActionResult> RemoveMember(string Id, string MemberId)
        //{
        //    //apply the function for delete a registration for a specific member
        //    //redirect to member views
        //    return View();
        //}

        //[Route("Familly/{Id}/DefineHead/{MemberId}")]
        //public async Task<ActionResult> DefineHead(string Id, string MemberId)
        //{
        //    //apply the function for Change the familly's head
        //    //redirect to member views
        //    return View();
        //}

        //[Route("Familly/{Id}/Exit")]
        //public async Task<ActionResult> Exit(string Id)
        //{
        //    string CurrentUser = User.FindFirst("UserId")?.Value;
        //    //apply the function for delete a registration for a specific member
        //    //redirect to member views
        //    return View();
        //}



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
