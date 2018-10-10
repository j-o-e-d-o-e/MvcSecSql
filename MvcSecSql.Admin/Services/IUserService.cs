using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MvcSecSql.Admin.Models;

namespace MvcSecSql.Admin.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUser(string userId);
        Task<IdentityResult> AddUser(CreateUserModel user);
        Task<bool> UpdateUser(UserModel user);
        Task<bool> DeleteUser(string userId);
    }
}