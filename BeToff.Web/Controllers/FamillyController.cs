using BeToff.BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeToff.Web.Controllers
{
    public class FamillyController : Controller
    {
        private readonly IFamillyBc _famillyBc;

        public FamillyController(IFamillyBc famillyBc)
        {
            _famillyBc = famillyBc;
        }
       

        public ActionResult Index()
        {
            Guid CurrentUserId = Guid.Parse(User.FindFirst("UserId").Value);
            ViewData["UserId"] = CurrentUserId;
            return View();
        }

        
        public ActionResult Details()
        {
            return View();
        }

        public async Task<ActionResult> Details(string id)
        {
            var Items = await _famillyBc.SelectFamilly(id);
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create()
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
