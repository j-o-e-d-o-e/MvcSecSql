using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Instructors
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        [BindProperty]
        public Band Input { get; set; } = new Band();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<Band>(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Update(Input);

                if (success)
                {
                    StatusMessage = $"Updated Band: {Input.Name}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}