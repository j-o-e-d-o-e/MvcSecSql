using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using MvcSecSql.Admin.Models;
using MvcSecSql.Admin.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.UserGenres
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;
        private readonly IDbReadService _dbReadService;
        private readonly IUserService _userService;

        [BindProperty]
        public UserCourseModel Input { get; set; } = new UserCourseModel();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService, IUserService userService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
            _userService = userService;
        }

        public void OnGet(int courseId, string userId)
        {
            ViewData["Genres"] = _dbReadService.GetSelectList<Genre>("Id", "Title");
            Input.UserGenre = _dbReadService.Get<UserGenre>(userId, courseId);
            Input.UpdatedUserGenre = Input.UserGenre;
            var course = _dbReadService.Get<Genre>(courseId);
            var user = _userService.GetUser(userId);
            Input.CourseTitle = course.Title;
            Input.Email = user.Email;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Update(Input.UserGenre, Input.UpdatedUserGenre);

                if (success)
                {
                    var updatedCourse = _dbReadService.Get<Genre>(Input.UpdatedUserGenre.GenreId);
                    StatusMessage = $"The [{Input.CourseTitle} | {Input.Email}] combination was changed to [{updatedCourse.Title} | {Input.Email}].";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["Genres"] = _dbReadService.GetSelectList<Genre>("Id", "Title");
            return Page();
        }

    }
}