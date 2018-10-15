using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Videos
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        [BindProperty]
        public Video Input { get; set; } = new Video();

        [TempData]
        public string StatusMessage { get; set; }

        public CreateModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbReadService = dbReadService;
            _dbWriteService = dbWriteService;
        }

        public void OnGet()
        {
            ViewData["Genres"] = _dbReadService.GetSelectList<Album>("Id", "Title");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
//                Input.BandId = _dbReadService.Get<Album>(Input.AlbumId).BandId; todo
                var success = await _dbWriteService.Add(Input);
                if (success)
                {
                    StatusMessage = $"Created a new Video: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}