using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.Courses
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IDbWriteService _dbWriteService;
        private IDbReadService _dbReadService;

        [BindProperty]
        public Course Input { get; set; } = new Course();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            ViewData["Instructors"] = _dbReadService.GetSelectList<Instructor>("Id", "Name");
            Input = _dbReadService.Get<Course>(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Update(Input);

                if (success)
                {
                    StatusMessage = $"Updated Course: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}