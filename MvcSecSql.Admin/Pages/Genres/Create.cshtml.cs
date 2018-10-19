using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcSecSql.Admin.Pages.Genres
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;

        [BindProperty]
        public Genre Input { get; set; } = new Genre();

        [TempData]
        public string StatusMessage { get; set; }

        public CreateModel(IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
        }

        public void OnGet()
        {
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
            var success = await _dbWriteService.Add(Input);
            if (!success) return Page();
            StatusMessage = $"Created a new Genre: {Input.Title}.";
            return RedirectToPage("Index");
        }
    }
}