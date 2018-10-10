using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcSecSql.Admin.Models;
using MvcSecSql.Data.Data;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Admin.Services
{
    public class UserService : IUserService
    {
        private VodContext _db;
        private readonly UserManager<User> _userManager;

        public UserService(VodContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return from user in _db.Users
                orderby user.Email
                select new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id)
                                                      && ur.RoleId.Equals(1.ToString()))
                };
        }

        public UserModel GetUser(string userId)
        {
            return (from user in _db.Users
                where user.Id.Equals(userId)
                select new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsAdmin = _db.UserRoles.Any(ur =>
                        ur.UserId.Equals(user.Id) && ur.RoleId.Equals(1.ToString()))
                }).FirstOrDefault();
        }

        public async Task<IdentityResult> AddUser(CreateUserModel user)
        {
            var dbUser = new User
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(dbUser, user.Password);
            return result;
        }

        public async Task<bool> UpdateUser(UserModel user)
        {
            var dbUser = await _db.Users.FirstOrDefaultAsync(u =>
                u.Id.Equals(user.Id));
            if (dbUser == null) return false;
            if (string.IsNullOrEmpty(user.Email)) return false;
            dbUser.Email = user.Email;

            var userRole = new IdentityUserRole<string>()
            {
                RoleId = "1",
                UserId = user.Id
            };
            var isAdmin = await _db.UserRoles.AnyAsync(ur => ur.Equals(userRole));
            if (isAdmin && !user.IsAdmin) //so far admin, but not in the future
                _db.UserRoles.Remove(userRole);
            else if (!isAdmin && user.IsAdmin) //so far not an admin, but for the future
                await _db.UserRoles.AddAsync(userRole);

            var result = await _db.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var dbUser = await _db.Users.FirstOrDefaultAsync(d => d.Id.Equals(userId));
                if (dbUser == null) return false;

                var userRoles = _db.UserRoles.Where(ur => ur.UserId.Equals(dbUser.Id));
                var result = await _db.SaveChangesAsync();
                if (result < 0) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}