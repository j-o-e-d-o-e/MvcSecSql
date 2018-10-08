using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcSecSql.Admin.Models;
using MvcSecSql.Admin.Services;

namespace MvcSecSql.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IEnumerable<UserPageModel> Users = new List<UserPageModel>();
        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}