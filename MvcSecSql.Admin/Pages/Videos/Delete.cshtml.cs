using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Admin.Pages.Videos
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IDbWriteService _dbWriteService;
        private IDbReadService _dbReadService;

        [BindProperty] public Video Input { get; set; } = new Video();
        [TempData] public string StatusMessage { get; set; }

        public DeleteModel(IDbReadService dbReadService,
            IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<Video>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Delete(Input);

                if (success)
                {
                    StatusMessage = $"Deleted Video: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}