using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Admin.Models;
using MvcSecSql.Admin.Services;

namespace MvcSecSql.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public CreateUserModel Input { get; set; } = new CreateUserModel();

        [TempData]
        public string StatusMessage { get; set; }

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddUser(Input);
                if (result.Succeeded)
                {
                    StatusMessage = $"Created a new account for {Input.Email}.";
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}