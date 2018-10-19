using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Services;
using MvcSecSql.Data.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.Bandmembers
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IDbWriteService _dbWriteService;
        private readonly IDbReadService _dbReadService;

        [BindProperty]
        public BandMember Input { get; set; } = new BandMember();

        [TempData]
        public string StatusMessage { get; set; }

        public DeleteModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            Input = _dbReadService.GetWithIncludes<BandMember>().FirstOrDefault(bandMember => bandMember.Id.Equals(id));
//            Input = _dbReadService.Get<BandMember>(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Delete(Input);

                if (success)
                {
                    StatusMessage = $"Deleted Bandmember: {Input.FirstName} {Input.LastName}.";
                    return RedirectToPage("Index");
                }
            }

            return RedirectToPage("Delete");
        }

    }
}