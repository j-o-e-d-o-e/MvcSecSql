using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Bandmembers
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        public DeleteModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        [BindProperty]
        public BandMember Input { get; set; } = new BandMember();

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet(int id)
        {
            Input = _dbReadService.Get<BandMember>(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage("Delete");
            var success = await _dbWriteService.Delete(Input);
            if (!success) return RedirectToPage("Delete");
            StatusMessage = $"Deleted Bandmember: {Input.FirstName} {Input.LastName}.";
            return RedirectToPage("Index");
        }
    }
}