using BeToff.BLL.Interface;
using BeToff.Entities;
using BeToff.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Security.Claims;

namespace BeToff.Web.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly IPhotoBc _photoBc;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly int _MaxBufferSize = 512 * 1024 * 1024;
        private readonly List<string> _ExtensionAuthorized = [".png", ".jpg", ".jpeg" ];  
        

        public PhotoController(IPhotoBc photoBc, IWebHostEnvironment hostEnvironment)
        {
            _photoBc = photoBc;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            string IdAuthor = User.FindFirst("UserId")?.Value;
            var ListPicture = await _photoBc.ListPhotoForSpecificUser(IdAuthor);


            var model = new PhotoListViewModel
            {
                Items = ListPicture,
                Count = ListPicture.Count
            };
            return View(model);
        }

        public IActionResult AddPhoto() {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoto(string Title, IFormFile image)
        {
            ViewData["ErrorMessage"] = "";

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


            using( var Stream = new FileStream(PathFile, FileMode.Create))
            {
                await image.CopyToAsync(Stream);
            }

            Photo photo = new Photo
            {
                Id = Guid.NewGuid(),
                Title = Title,
                AuthorId = Guid.Parse(User.FindFirst("UserId")?.Value),
                DateCreation = DateOnly.FromDateTime(DateTime.Now),
                Image = PathFile
            };

            var result = await _photoBc.SavePhoto(photo);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> DisplayPicture(string Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return NotFound();
            }
            Photo result = await _photoBc.GetSpecificPhoto(Id);

            if (result == null || string.IsNullOrEmpty(result.Image))
            {
                return NotFound(); // ou return View("Error"), ou un fallback
            };

            string FileName = Path.GetFileName(result.Image);

            var model = new PhotoViewModel
            {
                Image = result,
                FileName = FileName
            };

            return View(model);
        }

        public async Task<IActionResult> DeletePicture(string Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return NotFound();
            };

            var result = await _photoBc.GetSpecificPhoto(Id);

            if (result == null)
            {
                return NotFound();
            }

            string filePath = result.Image;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            else
            {
                return NotFound();
            }

            await _photoBc.DeleteSpecificPhoto(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
