using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Admin.Models;
using MvcSecSql.Admin.Services;

namespace MvcSecSql.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserModel Input { get; set; } = new UserModel();

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet(string userId)
        {
            Input = _userService.GetUser(userId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _userService.UpdateUser(Input);
            if (!result) return Page();
            StatusMessage = $"User {Input.Email} was updated.";
            return RedirectToPage("Index");
        }
    }
}