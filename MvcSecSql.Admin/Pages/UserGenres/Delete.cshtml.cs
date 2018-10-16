using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Admin.Services;
using MvcSecSql.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.UserGenres
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;
        private readonly IDbReadService _dbReadService;
        private readonly IUserService _userService;

        [BindProperty]
        public UserCourseModel Input { get; set; } = new UserCourseModel();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view
        
        public DeleteModel(IDbReadService dbReadService, IDbWriteService dbWriteService, IUserService userService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
            _userService = userService;
        }

        public void OnGet(int courseId, string userId)
        {
            var user = _userService.GetUser(userId);
            var course = _dbReadService.Get<Genre>(courseId);
            Input.UserGenre = _dbReadService.Get<UserGenre>(userId, courseId);
            Input.Email = user.Email;
            Input.CourseTitle = course.Title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Delete(Input.UserGenre);

                if (success)
                {
                    StatusMessage = $"User-Band combination [{Input.CourseTitle} | {Input.Email}] was deleted.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}