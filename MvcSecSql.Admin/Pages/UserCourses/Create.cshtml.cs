using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Admin.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.UserCourses
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private IDbReadService _dbReadService;
        private IDbWriteService _dbWriteService;
        private IUserService _userService;

        [BindProperty]
        public UserGenre Input { get; set; } = new UserGenre();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public CreateModel(IDbReadService dbReadService, IDbWriteService dbWriteService, IUserService userService)
        {
            _dbReadService = dbReadService;
            _dbWriteService = dbWriteService;
            _userService = userService;
        }

        public void OnGet()
        {
            ViewData["Users"] = _dbReadService.GetSelectList<User>("Id", "Email");
            ViewData["Genres"] = _dbReadService.GetSelectList<Genre>("Id", "Title");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Add(Input);

                if (success)
                {
                    var user = _userService.GetUser(Input.UserId);
                    var course = _dbReadService.Get<Genre>(Input.GenreId);
                    StatusMessage = $"User-Band combination [{course.Title} | {user.Email}] was created.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["Users"] = _dbReadService.GetSelectList<User>("Id", "Email");
            ViewData["Genres"] = _dbReadService.GetSelectList<Genre>("Id", "Title");
            return Page();
        }
    }
}