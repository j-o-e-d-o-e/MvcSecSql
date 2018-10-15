using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Ui.Models;
using MvcSecSql.UI.Repositories;

namespace MvcSecSql.Ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public HomeController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
//            if (!_signInManager.IsSignedIn(User))
//                return RedirectToAction("Login", "Account");
            return RedirectToAction("Genre", "Membership");
        }

//        public object Index()
//        {
//            var rep = new MockReadRepository();
//            const string userId = "5ebbf9f5-e4ed-4250-bc2c-e03a961c6a45";
//            return rep.GetGenres(userId);
////            return rep.GetGenre(userId, 1);
////            return rep.GetVideo(userId, 1);
////            return rep.GetVideos(userId, 9);
////            return rep.GetVideos(userId, 0);
//        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}