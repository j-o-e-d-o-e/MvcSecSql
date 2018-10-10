using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Admin.Services;
using MvcSecSql.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.UserCourses
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IDbWriteService _dbWriteService;
        private IDbReadService _dbReadService;
        private IUserService _userService;

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
            var course = _dbReadService.Get<Course>(courseId);
            Input.UserCourse = _dbReadService.Get<UserCourse>(userId, courseId);
            Input.Email = user.Email;
            Input.CourseTitle = course.Title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Delete(Input.UserCourse);

                if (success)
                {
                    StatusMessage = $"User-Course combination [{Input.CourseTitle} | {Input.Email}] was deleted.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}