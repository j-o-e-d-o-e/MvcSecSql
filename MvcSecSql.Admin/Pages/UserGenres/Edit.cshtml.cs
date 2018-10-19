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
        public UseGenreModel Input { get; set; } = new UseGenreModel();

        [TempData]
        public string StatusMessage { get; set; }

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
            Input.GenreTitle = _dbReadService.Get<Genre>(courseId).Title;
            Input.Email = _userService.GetUser(userId).Email;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Update(Input.UserGenre, Input.UpdatedUserGenre);

                if (success)
                {
                    var updatedCourse = _dbReadService.Get<Genre>(Input.UpdatedUserGenre.GenreId);
                    StatusMessage = $"The {Input.GenreTitle}/{Input.Email} combination was changed to: {updatedCourse.Title}/{Input.Email}.";
                    return RedirectToPage("Index");
                }
            }
            ViewData["Genres"] = _dbReadService.GetSelectList<Genre>("Id", "Title");
            return Page();
        }

    }
}