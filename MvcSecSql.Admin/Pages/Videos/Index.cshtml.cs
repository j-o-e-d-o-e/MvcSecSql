using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Videos
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        public IEnumerable<Video> Items = new List<Video>();

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IDbReadService dbReadService)
        {
            _dbReadService = dbReadService;
        }

        public void OnGet()
        {
            var videos = _dbReadService.GetWithIncludes<Video>();
            var enumerable = videos as Video[] ?? videos.ToArray();
            foreach(var video in enumerable)
            {
                video.Album.Band = _dbReadService.Get<Band>(video.Album.BandId);
            }
            Items = enumerable;
        }
    }
}