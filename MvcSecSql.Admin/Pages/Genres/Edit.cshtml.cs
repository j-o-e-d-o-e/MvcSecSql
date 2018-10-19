using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcSecSql.Admin.Pages.Genres
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;
        private readonly IDbReadService _dbReadService;

        [BindProperty]
        public Genre Input { get; set; } = new Genre();

        [TempData]
        public string StatusMessage { get; set; }

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<Genre>(id);
            ViewData["Images"] = new SelectList(
                new List<string>
                {
                    "/images/genre1.jpg",
                    "/images/genre2.jpg",
                    "/images/genre3.jpg",
                });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var success = await _dbWriteService.Update(Input);
            if (!success) return Page();
            StatusMessage = $"Updated Genre: {Input.Title}.";
            return RedirectToPage("Index");
        }
    }
}