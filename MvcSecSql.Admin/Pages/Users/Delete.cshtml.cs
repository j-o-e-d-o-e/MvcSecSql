using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Admin.Services;
using MvcSecSql.Admin.Models;

namespace MvcSecSql.Admin.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private IUserService _userService;

        [BindProperty]
        public UserModel Input { get; set; } = new UserModel();

        [TempData]
        public string StatusMessage { get; set; }

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet(string userId)
        {
            Input = _userService.GetUser(userId);
            StatusMessage = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.DeleteUser(Input.Id);

                if (result)
                {
                    StatusMessage = $"User {Input.Email} was deleted.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}