using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace MvcSecSql.Admin.Pages.UserGenres
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IDbReadService _dbReadService;
        public IEnumerable<UserGenre> Items = new List<UserGenre>();

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IDbReadService dbReadService)
        {
            _dbReadService = dbReadService;
        }

        public void OnGet()
        {
            Items = _dbReadService.GetWithIncludes<UserGenre>();
        }
    }
}