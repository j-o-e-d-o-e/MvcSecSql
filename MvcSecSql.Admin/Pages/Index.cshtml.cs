using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Admin.Models;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IDbReadService _db;

        public ( CardViewModel users, CardViewModel userGenres, CardViewModel genres,
            CardViewModel bands, CardViewModel albums, CardViewModel albumInfos,
            CardViewModel videos) Cards;

        public IndexModel(IDbReadService db)
        {
            _db = db;
        }

        public void OnGet()
        {
            var count = _db.Count();
            Cards = (
                users: new CardViewModel
                {
                    BackgroundColor = "#414141",
                    Count = count.users,
                    Description = "Users",
                    Icon = "education",
                    Url = "./Users/Index"
                },
                userGenres: new CardViewModel
                {
                    BackgroundColor = "#176c37",
                    Count = count.userGenres,
                    Description = "User Genres",
                    Icon = "file",
                    Url = "./UserGenres/Index"
                },
                genres: new CardViewModel
                {
                    BackgroundColor = "#009688",
                    Count = count.genres,
                    Description = "Genres",
                    Icon = "blackboard",
                    Url = "./Genres/Index"
                },
                bands: new CardViewModel
                {
                    BackgroundColor = "#9c27b0",
                    Count = count.bands,
                    Description = "Bands",
                    Icon = "user",
                    Url = "./Bands/Index"
                },
                albums: new CardViewModel
                {
                    BackgroundColor = "#f44336",
                    Count = count.albums,
                    Description = "Genres",
                    Icon = "list",
                    Url = "./Genres/Index"
                },
                albumInfos: new CardViewModel
                {
                    BackgroundColor = "#ffcc00",
                    Count = count.albumInfos,
                    Description = "Infos",
                    Icon = "file",
                    Url = "./Infos/Index"
                },
                videos: new CardViewModel
                {
                    BackgroundColor = "#3f51b5",
                    Count = count.videos,
                    Description = "Videos",
                    Icon = "film",
                    Url = "./Videos/Index"
                }
            );
        }
    }
}