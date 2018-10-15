﻿using System.Collections.Generic;
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
            var description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            var email = "a@b.c";
            var adminRoleId = string.Empty;
            var userId = string.Empty;

            if (context.Users.Any(r => r.Email.Equals(email)))
                userId = context.Users.First(r => r.Email.Equals(email)).Id;

            if (!userId.Equals(string.Empty))
            {
                if (!context.Bands.Any())
                {
                    var instructors = new List<Band>
                    {
                        new Band
                        {
                            Name = "John Doe",
                            Description = description.Substring(20, 50),
                            Image = "/images/Ice-Age-Scrat-icon.png"
                        },
                        new Band
                        {
                            Name = "Jane Doe",
                            Description = description.Substring(30, 40),
                            Image = "/images/Ice-Age-Scrat-icon.png"
                        }
                    };
                    context.Bands.AddRange(instructors);
                    context.SaveChanges();
                }

                if (!context.Genres.Any())
                {
                    var instructorId1 = context.Bands.First().Id;
                    var instructorId2 = int.MinValue;
                    var instructor = context.Bands.Skip(1).FirstOrDefault();
                    if (instructor != null) instructorId2 = instructor.Id;
                    else instructorId2 = instructorId1;

                    var courses = new List<Genre>
                    {
                        new Genre
                        {
//                            BandId = instructorId1, todo
                            Title = "Course 1",
                            Description = description,
                            ImageUrl = "/images/course.jpg",
                            MarqueeImageUrl = "/images/laptop.jpg"
                        },
                        new Genre
                        {
//                            BandId = instructorId2, todo
                            Title = "Course 2",
                            Description = description,
                            ImageUrl = "/images/course2.jpg",
                            MarqueeImageUrl = "/images/laptop.jpg"
                        },
                        new Genre
                        {
//                            BandId = instructorId1, todo
                            Title = "Course 3",
                            Description = description,
                            ImageUrl = "/images/course3.jpg",
                            MarqueeImageUrl = "/images/laptop.jpg"
                        }
                    };
                    context.Genres.AddRange(courses);
                    context.SaveChanges();
                }

                var courseId1 = int.MinValue;
                var courseId2 = int.MinValue;
                var courseId3 = int.MinValue;
                if (context.Genres.Any())
                {
                    courseId1 = context.Genres.First().Id;

                    var course = context.Genres.Skip(1).FirstOrDefault();
                    if (course != null) courseId2 = course.Id;

                    course = context.Genres.Skip(2).FirstOrDefault();
                    if (course != null) courseId3 = course.Id;
                }

                if (!context.UserGenres.Any())
                {
                    if (!courseId1.Equals(int.MinValue))
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = courseId1});

                    if (!courseId2.Equals(int.MinValue))
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = courseId2});

                    if (!courseId3.Equals(int.MinValue))
                        context.UserGenres.Add(new UserGenre
                            {UserId = userId, GenreId = courseId3});

                    context.SaveChanges();
                }

                if (!context.Albums.Any())
                {
                    var modules = new List<Album>
                    {
                        new Album {BandId = courseId1, Title = "Module 1"},
                        new Album {BandId = courseId1, Title = "Module 2"},
                        new Album {BandId = courseId2, Title = "Module 3"}
                    };
                    context.Albums.AddRange(modules);
                    context.SaveChanges();
                }

                var moduleId1 = int.MinValue;
                var moduleId2 = int.MinValue;
                var moduleId3 = int.MinValue;
                if (context.Albums.Any())
                {
                    moduleId1 = context.Albums.First().Id;

                    var module = context.Albums.Skip(1).FirstOrDefault();
                    if (module != null) moduleId2 = module.Id;
                    else moduleId2 = moduleId1;

                    module = context.Albums.Skip(2).FirstOrDefault();
                    if (module != null) moduleId3 = module.Id;
                    else moduleId3 = moduleId1;
                }

                if (!context.Videos.Any())
                {
                    var videos = new List<Video>
                    {
                        new Video
                        {
                            AlbumId = moduleId1,
//                            BandId = courseId1,
                            Position = 1, Title = "Video 1 Title",
                            Description = description.Substring(1, 35),
                            Duration = 50, Thumbnail = "/images/video1.jpg",
                            Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        },
                        new Video
                        {
                            AlbumId = moduleId1,
//                            BandId = courseId1,
                            Position = 2,
                            Title = "Video 2 Title",
                            Description = description.Substring(5, 35),
                            Duration = 45,
                            Thumbnail = "/images/video2.jpg",
                            Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        },
                        new Video
                        {
                            AlbumId = moduleId1,
//                            BandId = courseId1,
                            Position = 3,
                            Title = "Video 3 Title",
                            Description = description.Substring(10, 35),
                            Duration = 41,
                            Thumbnail = "/images/video3.jpg",
                            Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        },
                        new Video
                        {
                            AlbumId = moduleId3,
//                            BandId = courseId2,
                            Position = 1,
                            Title = "Video 4 Title",
                            Description = description.Substring(15, 35),
                            Duration = 41,
                            Thumbnail = "/images/video4.jpg",
                            Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        },
                        new Video
                        {
                            AlbumId = moduleId2,
//                            BandId = courseId1,
                            Position = 1,
                            Title = "Video 5 Title",
                            Description = description.Substring(20, 35),
                            Duration = 42,
                            Thumbnail = "/images/video5.jpg",
                            Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        }
                    };
                    context.Videos.AddRange(videos);
                    context.SaveChanges();
                }

                if (!context.AlbumInfos.Any())
                {
                    var downloads = new List<AlbumInfo>
                    {
                        new AlbumInfo
                        {
                            AlbumId = moduleId1,
//                            GenreId = courseId1, todo
                            Title = "ADO.NET 1 (PDF)", Url = "https://1drv.ms/b/s!AuD5OaH0ExAwn48rX9TZZ3kAOX6Peg"
                        },
                        new AlbumInfo
                        {
                            AlbumId = moduleId1,
//                            GenreId = courseId1, todo
                            Title = "ADO.NET 2 (PDF)", Url = "https://1drv.ms/b/s!AuD5OaH0ExAwn48rX9TZZ3kAOX6Peg"
                        },
                        new AlbumInfo
                        {
                            AlbumId = moduleId3,
//                            GenreId = courseId2, todo
                            Title = "ADO.NET 1 (PDF)", Url = "https://1drv.ms/b/s!AuD5OaH0ExAwn48rX9TZZ3kAOX6Peg"
                        }
                    };

                    context.AlbumInfos.AddRange(downloads);
                    context.SaveChanges();
                }
            }
        }
    }
}