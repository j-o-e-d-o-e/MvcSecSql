using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Admin.Models;
using MvcSecSql.Admin.Services;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Videos
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IDbReadService _dbReadService;
        private IDbWriteService _dbWriteService;

        [BindProperty]
        public Video Input { get; set; } = new Video();

        [TempData]
        public string StatusMessage { get; set; }

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbReadService = dbReadService;
            _dbWriteService = dbWriteService;
        }

        public void OnGet(int id)
        {
            ViewData["Modules"] = _dbReadService.GetSelectList<Module>("Id", "Title");
            Input = _dbReadService.Get<Video>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Input.CourseId = _dbReadService.Get<Module>(Input.ModuleId).CourseId;
                Input.Course = null;
                var success = await _dbWriteService.Update(Input);
                
                if (success)
                {
                    StatusMessage = $"Video {Input.Title} was updated.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}