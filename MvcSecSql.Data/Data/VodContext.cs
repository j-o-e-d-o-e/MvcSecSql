using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Data.Data
{
    public class VodContext : IdentityDbContext<User>
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AlbumInfo> AlbumInfos { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<UserGenre> UserGenres { get; set; }
        public DbSet<Video> Videos { get; set; }

        public VodContext(DbContextOptions<VodContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //composite key
            //Todo: add another composite key for many-to-many
            builder.Entity<UserGenre>().HasKey(userGenre => new {userGenre.UserId, GenreId = userGenre.GenreId});
            //restrict cascading deletes
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}