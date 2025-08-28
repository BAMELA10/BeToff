using BeToff.BLL;
using BeToff.BLL.Dto.Request;
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
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace BeToff.Web.Controllers
{
    public class FamillyController : Controller
    {
        private readonly IFamillyBc _famillyBc;
        private readonly IRegistrationBc _registrationBc;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPhotoFamilyBc _photoFamilyBc;
        private readonly int _MaxBufferSize = 512 * 1024 * 1024;
        private readonly List<string> _ExtensionAuthorized = [".png", ".jpg", ".jpeg"];

        public FamillyController(IFamillyBc famillyBc, IRegistrationBc registrationBc, IWebHostEnvironment hostEnvironment, IPhotoFamilyBc photoFamilyBc)
        {
            _famillyBc = famillyBc;
            _registrationBc = registrationBc;
            _hostEnvironment = hostEnvironment;
            _photoFamilyBc = photoFamilyBc;
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

        [Route("Familly/{Id}/Album")]
        public async Task<ActionResult> Album(string Id)
        {
            var Album = await _photoFamilyBc.GenerateAlbumForFamily(Id);
            var model = new PhotoFamillyListViewModel
            {
                Items = Album,
                Count = Album.Count
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

        [Route("Familly/{Id}/Home")]
        public async Task<ActionResult> Home(string Id)
        {
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

        [Route("Familly/{Id}/AddPhoto/")]
        public ActionResult AddPhoto(string Id)
        {
            return View();
        }
        [HttpPost]
        [Route("Familly/{Id}/AddPhoto/")]
        public async Task<ActionResult> AddPhoto(string Id, PhotoFamilyCreateViewModel Photo, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (image.Length > _MaxBufferSize)
            {
                ViewData["ErrorMessage"] = "the file's size is grather than 500 MB";
                return View();
            }
            if (!_ExtensionAuthorized.Contains(Path.GetExtension(image.FileName)))
            {
                ViewData["ErrorMessage"] = "UnAuthorized Extension";
                return View();
            }

            var FileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            Directory.CreateDirectory(Path.Combine(_hostEnvironment.WebRootPath, "Media"));

            var PathFile = Path.Combine(_hostEnvironment.WebRootPath, "Media", FileName);


            using (var Stream = new FileStream(PathFile, FileMode.Create))
            {
                await image.CopyToAsync(Stream);
            }

            var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var GuidUser = Guid.Parse(CurrentUser);
            var GuidFamilly = Guid.Parse(Id);

            var Dto = new PhotoFamillyCreateDto
            {
                Title = Photo.Title,
                Image = PathFile,
                FamilyId = GuidFamilly,
                AuthorId = GuidUser,
                DateCreation = DateOnly.FromDateTime(DateTime.Now),
            };

            await _photoFamilyBc.AddNewPhotoOnFamillyAlbum(Dto);

            return RedirectToAction("Home", new { Id = Id });
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

        [Route("Familly/{Id}/RemovePhoto/{PhotoId}")]
        public async Task<ActionResult> RemoveFamillyPicture(string Id, string PhotoId)
        {
            if (String.IsNullOrEmpty(PhotoId))
            {
                return BadRequest();
            };

            var result = await _photoFamilyBc.GetSpecificPcitureOfFamily(PhotoId, Id);

            if (result == null)
            {
                return BadRequest();
            }

            string filePath = result.Image;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            else
            {
                return BadRequest();
            }

            await _photoFamilyBc.RemovePhotoFromFamilyAlbum(PhotoId, Id); 
            return RedirectToAction("Album",new {Id = Id});
        }
    }
}
