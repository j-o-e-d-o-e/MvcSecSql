using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.Genres
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;
        private readonly IDbReadService _dbReadService;

        [BindProperty]
        public Genre Input { get; set; } = new Genre();

        [TempData]
        public string StatusMessage { get; set; }

        public DeleteModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<Genre>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
//                var genre = _dbReadService.Get<Genre>(Input.Id, true);
//                var userGenres = _dbReadService.Get<UserGenre>().Where(userGenre => userGenre.GenreId.Equals(genre.Id));

//                if (dbUser == null) return false;

//                var userGenres = _db.UserGenres.Where(userGenre => userGenre.UserId.Equals(dbUser.Id));
//                _db.UserGenres.RemoveRange(userGenres);
                var success = await _dbWriteService.Delete(Input);

                if (success)
                {
                    StatusMessage = $"Deleted Genre: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}