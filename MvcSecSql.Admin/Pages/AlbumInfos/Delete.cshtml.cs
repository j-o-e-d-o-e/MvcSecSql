using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.AlbumInfos
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        public DeleteModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        [BindProperty]
        public AlbumInfo Input { get; set; } = new AlbumInfo();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<AlbumInfo>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Delete(Input);

                if (success)
                {
                    StatusMessage = $"Deleted AlbumInfo: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}