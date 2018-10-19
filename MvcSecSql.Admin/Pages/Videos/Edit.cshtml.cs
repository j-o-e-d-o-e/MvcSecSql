using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Videos
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbReadService = dbReadService;
            _dbWriteService = dbWriteService;
        }

        [BindProperty]
        public Video Input { get; set; } = new Video();

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<Video>(id, true);
            Input.Album.Band = _dbReadService.Get<Band>(Input.Album.BandId);
            ViewData["Thumbnails"] = new SelectList(
                new List<string>
                {
                    "/images/video1.jpg",
                    "/images/video2.jpg",
                    "/images/video3.jpg",
                    "/images/video4.jpg",
                    "/images/video5.jpg"
                });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Input.Album = null;
            var success = await _dbWriteService.Update(Input);
            if (!success) return Page();
            StatusMessage = $"Video {Input.Title} was updated.";
            return RedirectToPage("Index");
        }
    }
}