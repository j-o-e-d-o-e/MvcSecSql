using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MvcSecSql.Admin.Models;

namespace MvcSecSql.Admin.Services
{
    public interface IUserService
    {
        IEnumerable<UserPageModel> GetUsers();
        UserPageModel GetUser(string userId);
        Task<IdentityResult> AddUser(RegisterUserPageModel user);
        Task<bool> UpdateUser(UserPageModel user);
        Task<bool> DeleteUser(string userId);
    }
}