using System.Collections.Generic;
using System.Linq;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.UI.Repositories
{
    public class MockReadRepository : IReadRepository
    {
        #region Mock Data

        List<Genre> _courses = new List<Genre>
        {
            new Genre
            {
                Id = 1, BandId = 1,
                MarqueeImageUrl = "/images/laptop.jpg",
                ImageUrl = "/images/course.jpg", Title = "C# For Beginners",
                Description = "Genre 1 Description: A very very long description."
            },
            new Genre
            {
                Id = 2, BandId = 1,
                MarqueeImageUrl = "/images/laptop.jpg",
                ImageUrl = "/images/course2.jpg", Title = "Programming C#",
                Description = "Genre 2 Description: A very very long description."
            },
            new Genre
            {
                Id = 3, BandId = 2,
                MarqueeImageUrl = "/images/laptop.jpg",
                ImageUrl = "/images/course3.jpg", Title = "MVC 5 For Beginners",
                Description = "Genre 3 Description: A very very long description."
            }
        };

        List<UserGenre> _userCourses = new List<UserGenre>
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
                UserId = "3fcd8c17-0a83-4c70-8b1c-9b2d4131a92f",
                GenreId = 3
            },
            new UserGenre
            {
                UserId = "00000000-0000-0000-0000-000000000000",
                GenreId = 1
            }
        };

        List<Album> _modules = new List<Album>
        {
            new Album {Id = 1, Title = "Album 1", GenreId = 1},
            new Album {Id = 2, Title = "Album 2", GenreId = 1},
            new Album {Id = 3, Title = "Album 3", GenreId = 2}
        };

        List<AlbumInfo> _downloads = new List<AlbumInfo>
        {
            new AlbumInfo
            {
                Id = 1, AlbumId = 1, GenreId = 1,
                Title = "ADO.NET 1 (PDF)",
                Url = "https://1drv.ms/b/s!AuD5OaH0ExAwn48rX9TZZ3kAOX6Peg"
            },
            new AlbumInfo
            {
                Id = 2, AlbumId = 1, GenreId = 1,
                Title = "ADO.NET 2 (PDF)",
                Url = "https://1drv.ms/b/s!AuD5OaH0ExAwn48rX9TZZ3kAOX6Peg"
            },
            new AlbumInfo
            {
                Id = 3, AlbumId = 3, GenreId = 2,
                Title = "ADO.NET 1 (PDF)",
                Url = "https://1drv.ms/b/s!AuD5OaH0ExAwn48rX9TZZ3kAOX6Peg"
            }
        };

        List<Band> _instructors = new List<Band>
        {
            new Band
            {
                Id = 1, Name = "John Doe",
                BandImage = "/images/Ice-Age-Scrat-icon.png",
                Description = "Lorem ipsum dolor sit amet, consectetur elit."
            },
            new Band
            {
                Id = 2, Name = "Jane Doe",
                BandImage = "/images/Ice-Age-Scrat-icon.png",
                Description = "Lorem ipsum dolor sit, consectetur adipiscing."
            }
        };

        List<Video> _videos = new List<Video>
        {
            new Video
            {
                Id = 1, AlbumId = 1, GenreId = 1, Position = 1,
                Title = "Video 1 Title", Description = "Video 1 Description: A very very long description.",
                Duration = 50, Thumbnail = "/images/video1.jpg", Url = "https://www.youtube.com/watch?v=GyFQAHc8oao"
            },
            new Video
            {
                Id = 2, AlbumId = 1, GenreId = 1, Position = 2,
                Title = "Video 2 Title", Description = "Video 2 Description: A very very long description.",
                Duration = 45, Thumbnail = "/images/video2.jpg", Url = "https://www.youtube.com/watch?v=0_e4YX73Ww4"
            },
            new Video
            {
                Id = 3, AlbumId = 3, GenreId = 2, Position = 1,
                Title = "Video 3 Title", Description = "Video 3 Description: A very very long description.",
                Duration = 41, Thumbnail = "/images/video3.jpg", Url = "https://www.youtube.com/watch?v=KfWccwZrrLg"
            },
            new Video
            {
                Id = 4, AlbumId = 2, GenreId = 1, Position = 1,
                Title = "Video 4 Title", Description = "Video 4 Description: A very very long description.",
                Duration = 42, Thumbnail = "/images/video4.jpg", Url = "https://www.youtube.com/watch?v=of2XNp6t_7c"
            },
            new Video
            {
                Id = 5, AlbumId = 1, GenreId = 1, Position = 3,
                Title = "Video 4 Title", Description = "Video 5 Description: A very very long description.",
                Duration = 91, Thumbnail = "/images/video5.jpg", Url = "https://www.youtube.com/watch?v=of2XNp6t_7c"
            }
        };

        #endregion

        public IEnumerable<Genre> GetCourses(string userId)
        {
            var courses = _userCourses.Where(uc => uc.UserId.Equals(userId))
                .Join(_courses, uc => uc.GenreId, c => c.Id, (uc, c) => new {Course = c})
                .Select(s => s.Course);

            foreach (var course in courses)
            {
                course.Band = _instructors.SingleOrDefault(s => s.Id.Equals(course.BandId));
                course.Albums = _modules.Where(m => m.GenreId.Equals(course.Id)).ToList();
            }

            return courses;
        }

        public Genre GetCourse(string userId, int courseId)
        {
            var course = _userCourses.Where(uc => uc.UserId.Equals(userId))
                .Join(_courses, uc => uc.GenreId, c => c.Id, (uc, c) => new {Course = c})
                .SingleOrDefault(s => s.Course.Id.Equals(courseId)).Course;

            course.Band = _instructors.SingleOrDefault(s => s.Id.Equals(course.BandId));

            course.Albums = _modules.Where(m => m.GenreId.Equals(course.Id)).ToList();

            foreach (var module in course.Albums)
            {
                module.Infos = _downloads.Where(d => d.AlbumId.Equals(module.Id)).ToList();
                module.Videos = _videos.Where(v => v.AlbumId.Equals(module.Id)).ToList();
            }

            return course;
        }

        public Video GetVideo(string userId, int videoId)
        {
            var video = _videos
                .Where(v => v.Id.Equals(videoId))
                .Join(_userCourses, v => v.GenreId, uc => uc.GenreId, (v, uc) => new {Video = v, UserCourse = uc})
                .Where(vuc => vuc.UserCourse.UserId.Equals(userId))
                .FirstOrDefault().Video;

            return video;
        }

        public IEnumerable<Video> GetVideos(string userId, int moduleId = 0)
        {
            var videos = _videos
                .Join(_userCourses, v => v.GenreId, uc => uc.GenreId, (v, uc) => new {Video = v, UserCourse = uc})
                .Where(vuc => vuc.UserCourse.UserId.Equals(userId));

            return moduleId.Equals(0)
                ? videos.Select(s => s.Video)
                : videos.Where(v => v.Video.AlbumId.Equals(moduleId)).Select(s => s.Video);
        }
    }
}