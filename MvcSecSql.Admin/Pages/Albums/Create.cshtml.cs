using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.Albums
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        [BindProperty]
        public Album Input { get; set; } = new Album();

        [TempData]
        public string StatusMessage { get; set; }

        public CreateModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbReadService = dbReadService;
            _dbWriteService = dbWriteService;
        }

        public void OnGet()
        {
            ViewData["Bands"] = _dbReadService.GetSelectList<Band>("Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Input.Band = _dbReadService.Get<Band>(Input.BandId, true);
            Input.Genre = _dbReadService.Get<Genre>(Input.Band.GenreId, false);
            if (ModelState.IsValid)
            {
            var success = await _dbWriteService.Add(Input);

            if (success)
            {
                StatusMessage = $"Created a new Album: {Input.Title}.";
                return RedirectToPage("Index");
            }
            }
            ViewData["Bands"] = _dbReadService.GetSelectList<Band>("Id", "Name");
            return Page();
        }
    }
}