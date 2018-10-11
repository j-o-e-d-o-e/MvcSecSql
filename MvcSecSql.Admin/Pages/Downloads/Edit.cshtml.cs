using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.Downloads
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IDbWriteService _dbWriteService;
        private IDbReadService _dbReadService;

        [BindProperty]
        public AlbumInfo Input { get; set; } = new AlbumInfo();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            ViewData["Genres"] = _dbReadService.GetSelectList<Album>("Id", "Title");
            Input = _dbReadService.Get<AlbumInfo>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Input.GenreId = _dbReadService.Get<Album>(Input.AlbumId).GenreId;
                Input.Genre = null;
                var success = await _dbWriteService.Update(Input);

                if (success)
                {
                    StatusMessage = $"Updated AlbumInfo: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}