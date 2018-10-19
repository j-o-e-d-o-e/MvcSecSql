using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.Albums
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;
        private readonly IDbReadService _dbReadService;

        [BindProperty]
        public Album Input { get; set; } = new Album();

        [TempData]
        public string StatusMessage { get; set; }

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<Album>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Input.Band = null;
            Input.Genre = null;
            var success = await _dbWriteService.Update(Input);
            if (!success) return Page();
            StatusMessage = $"Updated Album: {Input.Title}.";
            return RedirectToPage("Index");
        }
    }
}