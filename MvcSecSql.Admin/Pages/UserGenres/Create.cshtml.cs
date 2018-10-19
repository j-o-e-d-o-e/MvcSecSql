using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Admin.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.UserGenres
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;
        private readonly IUserService _userService;

        [BindProperty]
        public UserGenre Input { get; set; } = new UserGenre();

        [TempData]
        public string StatusMessage { get; set; }

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
            if (!ModelState.IsValid) return Page();
            var success = await _dbWriteService.Add(Input);
            if (!success) return Page();
            var genre = _dbReadService.Get<Genre>(Input.GenreId);
            var user = _userService.GetUser(Input.UserId);
            StatusMessage = $"User-Genre combination {genre.Title}/{user.Email} was created.";
            return RedirectToPage("Index");
        }
    }
}