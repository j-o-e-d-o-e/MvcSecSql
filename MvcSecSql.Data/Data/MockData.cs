using System.Collections.Generic;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Data.Data
{
    public class MockData
    {
        public static readonly List<UserGenre> UserGenres = new List<UserGenre>
        {
            new UserGenre
            {
                UserId = "5ebbf9f5-e4ed-4250-bc2c-e03a961c6a45",
                GenreId = 1
            },
            new UserGenre
            {
                UserId = "00000000-0000-0000-0000-000000000000",
                GenreId = 2
            },
            new UserGenre
            {
                UserId = "5ebbf9f5-e4ed-4250-bc2c-e03a961c6a45",
                GenreId = 3
            },
            new UserGenre
            {
                UserId = "00000000-0000-0000-0000-000000000000",
                GenreId = 1
            }
        };

        public static readonly List<Genre> Genres = new List<Genre>
        {
            new Genre
            {
                Id = 1, BandId = 1,
                MarqueeImageUrl = "/images/background.jpg",
                ImageUrl = "/images/genre1.jpg", Title = "Prog",
                Description = "Progressive metal: fusion, djent et al"
            },
            new Genre
            {
                Id = 2, BandId = 1,
                MarqueeImageUrl = "/images/background.jpg",
                ImageUrl = "/images/genre2.jpg", Title = "Death",
                Description = "Death metal: technical et al"
            },
            new Genre
            {
                Id = 3, BandId = 2,
                MarqueeImageUrl = "/images/background.jpg",
                ImageUrl = "/images/genre3.jpg", Title = "Classics",
                Description = "Metal music to remember"
            }
        };

        public static readonly List<Band> Bands = new List<Band>
        {
            new Band
            {
                Id = 1, Name = "Haken",
                BandImage = "/images/avatar.png",
                Description = "Formed in 2007"
            },
            new Band
            {
                Id = 2, Name = "Caligula's Horse",
                BandImage = "/images/avatar.png",
                Description = "Formed in 2011"
            }
        };

        public static readonly List<Album> Albums = new List<Album>
        {
            new Album {Id = 1, Title = "Visions", GenreId = 1},
            new Album {Id = 2, Title = "The Mountain", GenreId = 1},
            new Album {Id = 3, Title = "Affinity", GenreId = 2}
        };

        public static readonly List<AlbumInfo> AlbumInfos = new List<AlbumInfo>
        {
            new AlbumInfo
            {
                Id = 1, AlbumId = 1, GenreId = 1,
                Title = "Review",
                Url = "https://www.seaoftranquility.org/reviews.php?op=showcontent&id=19736"
            },
            new AlbumInfo
            {
                Id = 2, AlbumId = 2, GenreId = 1,
                Title = "Review",
                Url = "https://www.sputnikmusic.com/review/70605/Haken-The-Mountain/"
            },
            new AlbumInfo
            {
                Id = 3, AlbumId = 3, GenreId = 1,
                Title = "Review",
                Url = "https://progreport.com/haken-affinity-album-review/"
            }
        };

        public static readonly List<Video> Videos = new List<Video>
        {
            new Video
            {
                Id = 1, AlbumId = 1, GenreId = 1, Position = 1,
                Title = "Haken - Visions",
                Description =
                    "This video was made by Marc Papeghin, multi instrumentalist and producer from France who also made 'Flying Colors'﻿",
                Duration = 23, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=of2XNp6t_7c"
            },
            new Video
            {
                Id = 2, AlbumId = 1, GenreId = 1, Position = 2,
                Title = "Haken - Streams", Description = "Album track",
                Duration = 11, Thumbnail = "/images/video2.jpg", Url = "https://www.youtube.com/watch?v=UW4D6v75iH4"
            },
            new Video
            {
                Id = 3, AlbumId = 3, GenreId = 1, Position = 1,
                Title = "Haken - The Architect", Description = "Album track",
                Duration = 16, Thumbnail = "/images/video3.jpg", Url = "https://www.youtube.com/watch?v=coydAEGZMKk"
            },
            new Video
            {
                Id = 4, AlbumId = 2, GenreId = 1, Position = 1,
                Title = "Haken - Cockroach King", Description = "Live in Mexico",
                Duration = 9, Thumbnail = "/images/video4.jpg", Url = "https://www.youtube.com/watch?v=M-54x0Qz4og"
            },
            new Video
            {
                Id = 5, AlbumId = 2, GenreId = 1, Position = 3,
                Title = "Haken - Falling Back To Earth", Description = "Album Track",
                Duration = 12, Thumbnail = "/images/video5.jpg", Url = "https://www.youtube.com/watch?v=9XKPW0XVXe4"
            }
        };
    }
}