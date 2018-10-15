using System.Collections.Generic;
using System.Linq;
using MvcSecSql.Data.Data;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Data.Migrations
{
    public class DbInitializer
    {
        public static void RecreateDatabase(VodContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void Initialize(VodContext context)
        {
//            var adminRoleId = string.Empty;
            var userId = string.Empty;
            const string email = "a@b.c";
            if (context.Users.Any(user => user.Email.Equals(email)))
                userId = context.Users.First(user => user.Email.Equals(email)).Id;

            // ReSharper disable once InvertIf
            if (!userId.Equals(string.Empty))
            {
                if (!context.Bands.Any())
                {
                    var bands = MockData.Bands;
                    context.Bands.AddRange(bands);
                    context.SaveChanges();
                }

                if (!context.Genres.Any()) // table: Genres
                {
                    var bandId1 = context.Bands.First().Id;
                    var bandId2 = int.MinValue;
                    var instructor = context.Bands.Skip(1).FirstOrDefault();
                    if (instructor == null)
                        bandId2 = bandId1;
                    else
                        bandId2 = instructor.Id;

                    var genres = MockData.Genres;
                    context.Genres.AddRange(genres);
                    context.SaveChanges();
                }

                var genreId1 = int.MinValue;
                var genreId2 = int.MinValue;
                var genreId3 = int.MinValue;
                if (context.Genres.Any())
                {
                    genreId1 = context.Genres.First().Id;

                    var genre = context.Genres.Skip(1).FirstOrDefault();
                    if (genre != null) genreId2 = genre.Id;

                    genre = context.Genres.Skip(2).FirstOrDefault();
                    if (genre != null) genreId3 = genre.Id;
                }

                if (!context.UserGenres.Any()) // table: UserGenres
                {
                    if (!genreId1.Equals(int.MinValue))
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = genreId1});

                    if (!genreId2.Equals(int.MinValue))
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = genreId2});

                    if (!genreId3.Equals(int.MinValue))
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = genreId3});

                    context.SaveChanges();
                }

                if (!context.Bands.Any()) // table: Bands
                {

                    var bands = MockData.Bands;

                    context.Bands.AddRange(bands);
                    context.SaveChanges();
                }

                if (!context.BandMembers.Any()) // table: BandMembers
                {

                    var bandMembers = MockData.BandMembers;

                    context.BandMembers.AddRange(bandMembers);
                    context.SaveChanges();
                }

                if (!context.Albums.Any()) // table: Albums
                {
                    var albums = MockData.Albums;
                    context.Albums.AddRange(albums);
                    context.SaveChanges();
                }
                else
                {
                    var albumId1 = int.MinValue;
                    var albumId2 = int.MinValue;
                    var albumId3 = int.MinValue;
                    albumId1 = context.Albums.First().Id;

                    var module = context.Albums.Skip(1).FirstOrDefault();
                    if (module != null) albumId2 = module.Id;
                    else albumId2 = albumId1;

                    module = context.Albums.Skip(2).FirstOrDefault();
                    if (module != null) albumId3 = module.Id;
                    else albumId3 = albumId1;
                }

                if (!context.AlbumInfos.Any()) // table: AlbumInfos
                {
                    var albumInfos = MockData.AlbumInfos;

                    context.AlbumInfos.AddRange(albumInfos);
                    context.SaveChanges();
                }

                if (!context.Videos.Any()) // table: Videos
                {
                    var videos = MockData.Videos;
                    context.Videos.AddRange(videos);
                    context.SaveChanges();
                }
            }
        }
    }
}