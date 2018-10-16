using System;
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
            var userId = string.Empty;
            const string email = "a@b.c";
            if (context.Users.Any(user => user.Email.Equals(email)))
                userId = context.Users.First(user => user.Email.Equals(email)).Id;

            // ReSharper disable once InvertIf
            if (!userId.Equals(string.Empty))
            {
                if (!context.Genres.Any()) // table: Genres
                {
                    var genres = new List<Genre>
                    {
                        new Genre
                        {
                            Title = "Prog", Description = "Progressive metal: fusion, djent et al",
                            ImageUrl = "/images/genre1.jpg",
                            MarqueeImageUrl = "/images/background.jpg"
                        },
                        new Genre
                        {
                            Title = "Death", Description = "Death metal: technical et al",
                            ImageUrl = "/images/genre2.jpg",
                            MarqueeImageUrl = "/images/background.jpg"
                        }
                    };
                    context.Genres.AddRange(genres);
                    context.SaveChanges();
                }

                if (!context.UserGenres.Any()) // table: UserGenres
                {
                    context.UserGenres.Add(new UserGenre
                        {UserId = userId, GenreId = context.Genres.First().Id});

                    var genre = context.Genres.Skip(1).FirstOrDefault();
                    if (genre != null)
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = genre.Id});

                    genre = context.Genres.Skip(2).FirstOrDefault();
                    if (genre != null)
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = genre.Id});

                    context.SaveChanges();
                }

                if (!context.Bands.Any()) // table: Bands
                {
                    var bands = new List<Band>
                    {
                        new Band
                        {
                            Name = "Haken", GenreId = context.Genres.First().Id,
                            Description = "Formed in 2007", Image = "/images/avatar.png"
                        },
                        new Band
                        {
                            Name = "Caligula's Horse", GenreId = context.Genres.First().Id,
                            Description = "Formed in 2011", Image = "/images/avatar.png"
                        }
                    };
                    context.Bands.AddRange(bands);
                    context.SaveChanges();
                }

                if (!context.BandMembers.Any()) // table: BandMembers
                {
                    var bandMembers = new List<BandMember>
                    {
                        new BandMember
                        {
                            FirstName = "Mary", LastName = "Jane",
                            Instrument = "Vocals", BandId = context.Bands.First().Id,
                            Description = "Since 2010", Image = "/images/avatar.png"
                        },
                        new BandMember
                        {
                            FirstName = "Boris", LastName = "String",
                            Instrument = "Guitar", BandId = context.Bands.First().Id,
                            Description = "Since 2011", Image = "/images/avatar.png"
                        },
                        new BandMember
                        {
                            FirstName = "Carla", LastName = "Montepulciano",
                            Instrument = "Drums", BandId = context.Bands.First().Id,
                            Description = "Since 2012", Image = "/images/avatar.png"
                        },
                        new BandMember
                        {
                            FirstName = "Joe", LastName = "Doe",
                            Instrument = "Bass", BandId = context.Bands.First().Id,
                            Description = "Since 2013", Image = "/images/avatar.png"
                        }
                    };
                    context.BandMembers.AddRange(bandMembers);
                    context.SaveChanges();
                }

                if (!context.Albums.Any()) // table: Albums
                {
                    var bandId = int.MinValue;
                    var band = context.Bands.Skip(1).FirstOrDefault();
                    if (band != null) bandId = band.Id;
                    var albums = new List<Album>
                    {
                        new Album
                        {
                            Title = "Visions", BandId = context.Bands.First().Id,
                            ReleaseYear = 2011, GenreId = context.Bands.First().Id
                        },
                        new Album
                        {
                            Title = "The Mountain", BandId = context.Bands.First().Id,
                            ReleaseYear = 2013, GenreId = context.Genres.First().Id
                        },
                        new Album
                        {
                            Title = "In Contact", BandId = bandId,
                            ReleaseYear = 2017, GenreId = context.Genres.First().Id
                        },
                    };
                    context.Albums.AddRange(albums);
                    context.SaveChanges();
                }

                if (!context.AlbumInfos.Any()) // table: AlbumInfos
                {
                    var albumInfos = new List<AlbumInfo>
                    {
                        new AlbumInfo
                        {
                            AlbumId = context.Albums.First().Id, Title = "Review",
                            Url = "https://www.seaoftranquility.org/reviews.php?op=showcontent&id=19736"
                        }
                    };
                    context.AlbumInfos.AddRange(albumInfos);
                    context.SaveChanges();
                }

                // ReSharper disable once InvertIf
                if (!context.Videos.Any()) // table: Videos
                {
                    var albumId = int.MinValue;
                    var album = context.Albums.Skip(1).FirstOrDefault();
                    if (album != null) albumId = album.Id;
                    var videos = new List<Video>
                    {
                        new Video
                        {
                            AlbumId = context.Albums.First().Id, Position = 1,
                            Title = "Haken - Visions",
                            Description = "Made by Marc Papeghin, multi instrumentalist and producer from France﻿",
                            Duration = 23, Thumbnail = "/images/video1.jpg",
                            Url = "https://www.youtube.com/watch?v=of2XNp6t_7c"
                        },
                        new Video
                        {
                            AlbumId = context.Albums.First().Id, Position = 1,
                            Title = "Haken - Streams", Description = "Album track",
                            Duration = 11, Thumbnail = "/images/video2.jpg",
                            Url = "https://www.youtube.com/watch?v=UW4D6v75iH4"
                        },
                        new Video
                        {
                            AlbumId = albumId, Position = 1,
                            Title = "Haken - Cockroach King", Description = "Live in Mexico",
                            Duration = 9, Thumbnail = "/images/video3.jpg",
                            Url = "https://www.youtube.com/watch?v=M-54x0Qz4og"
                        }
                    };
                    context.Videos.AddRange(videos);
                    context.SaveChanges();
                }
            }
        }
    }
}