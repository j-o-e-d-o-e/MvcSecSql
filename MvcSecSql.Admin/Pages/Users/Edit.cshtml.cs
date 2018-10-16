using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using MvcSecSql.Admin.Models;
using MvcSecSql.Admin.Services;

namespace MvcSecSql.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public UserModel Input { get; set; } = new UserModel();

        [TempData]
        public string StatusMessage { get; set; }

        public EditModel(IUserService userService)
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
                var result = await _userService.UpdateUser(Input);

                if (result)
                {
                    StatusMessage = $"User {Input.Email} was updated.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}