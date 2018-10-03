using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Data.Data
{
    public class VodContext : IdentityDbContext<User>
    {
        public VodContext(DbContextOptions<VodContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}