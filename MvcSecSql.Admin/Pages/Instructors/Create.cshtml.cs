using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Instructors
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;

        [BindProperty]
        public Band Input { get; set; } = new Band();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view


        public CreateModel(IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Add(Input);

                if (success)
                {
                    StatusMessage = $"Created a new Bands: {Input.Name}.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}