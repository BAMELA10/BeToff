using BeToff.BLL.Interface;
using BeToff.BLL.Mapping;
using BeToff.BLL.Service.Interface;
using BeToff.Entities;
using BeToff.Web.Hubs;
using BeToff.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

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

            string CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            string CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            ViewData["CurrentUser"] = CurrentUser;
            var AllFamillyMember = await _registrationBc.ListOfRegistrationForFamilly(Id);
            var model = new RegistrationViewModel
            {
                Registrations = AllFamillyMember
            };
            //affect that list to his viewmodel for print it
            return View(model);
        }

        [Route("Familly/{Id}/RemoveMember/{MemberId}")]
        public async Task<ActionResult> RemoveMember(string Id, string MemberId)
        {
            string CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            //apply the function for delete a registration for a specific member
            await _registrationBc.RemoveSpecificFamillyMember(Id, MemberId, CurrentUser);
            //redirect to member views
            return RedirectToAction("Members", new { Id = Id });
        }

        [Route("Familly/{Id}/DefineHead/{MemberId}")]
        public async Task<ActionResult> DefineHead(string Id, string MemberId)
        {
            string CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            //Get current familly
            var item = await _famillyBc.SelectFamilly(Id);
            var FamillyDto = FamillyMapper.ToDto(item);
            //Check if Current User is the head of the familly
            if (CurrentUser.Equals(FamillyDto.IdHead.ToString()))
            {
                //apply the function for Change the familly's head
                await _famillyBc.ChangeHeadOfFamilly(Id, MemberId);
                //redirect to member views
                return RedirectToAction("Members", new { Id = Id });
            }
            else
            {
                throw new Exception("Unauthorized Action for this User");
            }
         
            
        }

        [Route("Familly/{Id}/Exit/")]
        public async Task<ActionResult> Exit(string Id)
        {
            string CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            //Get current familly
            var item = await _famillyBc.SelectFamilly(Id);
            var FamillyDto = FamillyMapper.ToDto(item);
            Console.WriteLine("idHead" + FamillyDto.IdHead.ToString());
            Console.WriteLine("CurrentUser" + CurrentUser);

            if (CurrentUser.Equals(FamillyDto.IdHead.ToString()))
            {
                Console.WriteLine("Operation for chief of familly");
                // Get Identifier for New Familly's Head
                var RandomNewHeadForFamilly = await _registrationBc.SelectRandomIdentiferMenberOfFamilly(Id);
                //define the new head of familly
                await _famillyBc.ChangeHeadOfFamilly(Id, RandomNewHeadForFamilly.ToString());
                //remove registration
                //apply the function for delete a registration for a specific member
                await _registrationBc.RemoveSpecificFamillyMember(Id, CurrentUser, CurrentUser);
                //redirect to Familly Index page
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //remove registration
                //apply the function for delete a registration for a specific member
                await _registrationBc.RemoveSpecificFamillyMember(Id, CurrentUser, CurrentUser);
                //redirect to Familly Index page
                return RedirectToAction(nameof(Index));
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string NameOfFamilly)
        {
            
            try
            {
                string CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
