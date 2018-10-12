using System.Collections.Generic;
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

        public IEnumerable<Genre> Index()
        {
            var rep = new MockReadRepository();
            var userId = "5ebbf9f5-e4ed-4250-bc2c-e03a961c6a45";
            return rep.GetGenres(userId);
            // return rep.GetGenre(userId, 1);
            // return rep.GetVideo(userId, 1);
            // return rep.GetVideos(userId, 1);
        }

//        public IActionResult Index()
//        {
//            if (!_signInManager.IsSignedIn(User))
//                return RedirectToAction("Login", "Account");
//            return RedirectToAction("Dashboard", "Membership");
//        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}