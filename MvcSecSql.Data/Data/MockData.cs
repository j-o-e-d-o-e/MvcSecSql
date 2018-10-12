﻿using System.Collections.Generic;
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
                Id = 1, Title = "Prog", Description = "Progressive metal: fusion, djent et al",
                ImageUrl = "/images/genre1.jpg", MarqueeImageUrl = "/images/background.jpg",
            },
            new Genre
            {
                Id = 2, Title = "Death", Description = "Death metal: technical et al",
                ImageUrl = "/images/genre2.jpg", MarqueeImageUrl = "/images/background.jpg",
            },
            new Genre
            {
                Id = 3, Title = "Classics", Description = "Influential and all-time-favorites",
                ImageUrl = "/images/genre3.jpg", MarqueeImageUrl = "/images/background.jpg",
            }
        };

        public static readonly List<GenreBand> GenreBands = new List<GenreBand>
        {
            new GenreBand
            {
                GenreId = 1,
                BandId = 1
            },
            new GenreBand
            {
                GenreId = 1,
                BandId = 2
            },
            new GenreBand
            {
                GenreId = 2,
                BandId = 3
            },
            new GenreBand
            {
                GenreId = 2,
                BandId = 4
            },
            new GenreBand
            {
                GenreId = 3,
                BandId = 5
            },
            new GenreBand
            {
                GenreId = 1,
                BandId = 6
            },
            new GenreBand
            {
                GenreId = 3,
                BandId = 6
            }
        };

        public static readonly List<Band> Bands = new List<Band>
        {
            new Band
            {
                Id = 1, Name = "Haken",
                Description = "Formed in 2007",
                BandImage = "/images/avatar.png",
            },
            new Band
            {
                Id = 2, Name = "Caligula's Horse",
                Description = "Formed in 2011",
                BandImage = "/images/avatar.png",
            },
            new Band
            {
                Id = 3, Name = "Nile",
                Description = "Formed in 1993",
                BandImage = "/images/avatar.png",
            },
            new Band
            {
                Id = 4, Name = "Immolation",
                Description = "Formed in 1986",
                BandImage = "/images/avatar.png",
            },
            new Band
            {
                Id = 5, Name = "Rush",
                Description = "Formed in 1986",
                BandImage = "/images/avatar.png",
            },
            new Band
            {
                Id = 6, Name = "Dream Theater",
                Description = "Formed in 1986",
                BandImage = "/images/avatar.png",
            }
        };

        public static readonly List<Album> Albums = new List<Album>
        {
            new Album {Id = 1, Title = "Visions", BandId = 1, ReleaseYear = 2011, GenreId = 1},
            new Album {Id = 2, Title = "The Mountain", BandId = 1, ReleaseYear = 2013, GenreId = 1},
            new Album {Id = 3, Title = "Vector", BandId = 1, ReleaseYear = 2018, GenreId = 1},
            new Album {Id = 4, Title = "In Contact", BandId = 2, ReleaseYear = 2017, GenreId = 1},
            new Album {Id = 5, Title = "In Their Darkened Shrines", BandId = 3, ReleaseYear = 2002, GenreId = 2},
            new Album {Id = 6, Title = "Atonement", BandId = 4, ReleaseYear = 2017, GenreId = 2},
            new Album {Id = 7, Title = "Moving Pictures", BandId = 5, ReleaseYear = 1981, GenreId = 3},
            new Album {Id = 8, Title = "Images and Words", BandId = 6, ReleaseYear = 1992, GenreId = 3}
        };

        public static readonly List<AlbumInfo> AlbumInfos = new List<AlbumInfo>
        {
            new AlbumInfo
            {
                Id = 1, AlbumId = 1, Title = "Review",
                Url = "https://www.seaoftranquility.org/reviews.php?op=showcontent&id=19736"
            },
            new AlbumInfo
            {
                Id = 2, AlbumId = 2, Title = "Review",
                Url = "https://www.sputnikmusic.com/review/70605/Haken-The-Mountain/"
            },
            new AlbumInfo
            {
                Id = 3, AlbumId = 3, Title = "Review",
                Url = "https://progreport.com/haken-vector-album-review/"
            }
        };

        public static readonly List<Video> Videos = new List<Video>
        {
            new Video
            {
                Id = 1, AlbumId = 1, GenreId = 1, Position = 1,
                Title = "Haken - Visions",
                Description = "Made by Marc Papeghin, multi instrumentalist and producer from France﻿",
                Duration = 23, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=of2XNp6t_7c"
            },
            new Video
            {
                Id = 2, AlbumId = 1, GenreId = 1, Position = 1,
                Title = "Haken - Streams", Description = "Album track",
                Duration = 11, Thumbnail = "/images/video2.jpg", Url = "https://www.youtube.com/watch?v=UW4D6v75iH4"
            },
            new Video
            {
                Id = 3, AlbumId = 2, GenreId = 1, Position = 1,
                Title = "Haken - Cockroach King", Description = "Live in Mexico",
                Duration = 9, Thumbnail = "/images/video3.jpg", Url = "https://www.youtube.com/watch?v=M-54x0Qz4og"
            },
            new Video
            {
                Id = 4, AlbumId = 2, GenreId = 1, Position = 1,
                Title = "Haken - Falling Back To Earth", Description = "Album Track",
                Duration = 12, Thumbnail = "/images/video4.jpg", Url = "https://www.youtube.com/watch?v=9XKPW0XVXe4"
            },
            new Video
            {
                Id = 5, AlbumId = 3, GenreId = 1, Position = 1,
                Title = "Haken - The Good Doctor", Description = "Official Music Video",
                Duration = 16, Thumbnail = "/images/video5.jpg", Url = "https://www.youtube.com/watch?v=BD3v8w57_lU"
            },
            new Video
            {
                Id = 6, AlbumId = 4, GenreId = 1, Position = 1,
                Title = "Caligula's Horse - The Hands Are The Hardest", Description = "Album Track",
                Duration = 5, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=3OeS3RbZL7U"
            },
            new Video
            {
                Id = 6, AlbumId = 5, GenreId = 1, Position = 1,
                Title = "Nile - Unas, Slayer of the Gods", Description = "Album Track",
                Duration = 12, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=HPYQB7AsSkg"
            },
            new Video
            {
                Id = 7, AlbumId = 6, GenreId = 2, Position = 1,
                Title = "Immolation - When The Jackals Come", Description = "Official Music Video",
                Duration = 4, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=1u7wZFkmaVA"
            },
            new Video
            {
                Id = 8, AlbumId = 7, GenreId = 3, Position = 1,
                Title = "Rush - YYZ", Description = "Album Track",
                Duration = 5, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=LdpMpfp-J_I"
            },
            new Video
            {
                Id = 9, AlbumId = 8, GenreId = 3, Position = 1,
                Title = "Dream Theater - Take The Time", Description = "Official Video",
                Duration = 6, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=C5sg8heGdyk"
            }
        };
    }
}