using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.Admin.Pages.Bandmembers
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        private readonly IDbWriteService _dbWriteService;

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
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
            ViewData["Band"] = _dbReadService.GetSelectList<Band>("Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var success = await _dbWriteService.Update(Input);
            if (!success) return Page();
            StatusMessage = $"Updated Bandmember: {Input.FirstName} {Input.LastName}.";
            return RedirectToPage("Index");
        }
    }
}