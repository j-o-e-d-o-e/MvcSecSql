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

        public (CardViewModel users, CardViewModel userGenres, CardViewModel genres,
            CardViewModel bands, CardViewModel bandmembers, CardViewModel albums,
            CardViewModel albumInfos, CardViewModel videos) Cards;

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
                    Icon = "user",
                    Url = "./Users/Index"
                },
                userGenres: new CardViewModel
                {
                    BackgroundColor = "#176c37",
                    Count = count.userGenres,
                    Description = "User Genres",
                    Icon = "list",
                    Url = "./UserGenres/Index"
                },
                genres: new CardViewModel
                {
                    BackgroundColor = "#009688",
                    Count = count.genres,
                    Description = "Genres",
                    Icon = "list-alt",
                    Url = "./Genres/Index"
                },
                bands: new CardViewModel
                {
                    BackgroundColor = "#9c27b0",
                    Count = count.bands,
                    Description = "Bands",
                    Icon = "music",
                    Url = "./Bands/Index"
                },
                bandmembers: new CardViewModel
                {
                    BackgroundColor = "grey",
                    Count = count.bandmembers,
                    Description = "Band Members",
                    Icon = "user",
                    Url = "./Bandmembers/Index"
                },
                albums: new CardViewModel
                {
                    BackgroundColor = "#f44336",
                    Count = count.albums,
                    Description = "Albums",
                    Icon = "music",
                    Url = "./Albums/Index"
                },
                albumInfos: new CardViewModel
                {
                    BackgroundColor = "#ffcc00",
                    Count = count.albumInfos,
                    Description = "Album Infos",
                    Icon = "file",
                    Url = "./AlbumInfos/Index"
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