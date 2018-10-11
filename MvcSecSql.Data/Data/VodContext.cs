using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Data.Data
{
    public class VodContext : IdentityDbContext<User>
    {
        public DbSet<Genre> Courses { get; set; }
        public DbSet<AlbumInfo> Downloads { get; set; }
        public DbSet<Band> Instructors { get; set; }
        public DbSet<Album> Modules { get; set; }
        public DbSet<UserGenre> UserCourses { get; set; }
        public DbSet<Video> Videos { get; set; }

        public VodContext(DbContextOptions<VodContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //composite key
            builder.Entity<UserGenre>().HasKey(uc => new {uc.UserId, CourseId = uc.GenreId});
            //restrict cascading deletes
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}