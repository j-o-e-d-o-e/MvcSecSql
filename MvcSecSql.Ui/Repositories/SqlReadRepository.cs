using System.Collections.Generic;
using System.Linq;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.UI.Repositories
{
    public class SqlReadRepository : IReadRepository
    {
        private IDbReadService _db;

        public SqlReadRepository(IDbReadService db)
        {
            _db = db;
        }

        public Genre GetGenre(string userId, int genreId)
        {
            var hasAccess = _db.Get<UserGenre>(userId, genreId) != null;
            if (!hasAccess) return default(Genre);

            var course = _db.Get<Genre>(genreId, true);

            foreach (var module in course.Albums)
            {
                module.Infos = _db.Get<AlbumInfo>().Where(d =>
                    d.AlbumId.Equals(module.Id)).ToList();

                module.Videos = _db.Get<Video>().Where(d =>
                    d.AlbumId.Equals(module.Id)).ToList();
            }

            return course;
        }

        public IEnumerable<Genre> GetGenres(string userId)
        {
            var courses = _db.GetWithIncludes<UserGenre>()
                .Where(uc => uc.UserId.Equals(userId))
                .Select(c => c.Genre);

            return courses;
        }

        public Video GetVideo(string userId, int videoId)
        {
            var video = _db.Get<Video>(videoId);

            var hasAccess = _db.Get<UserGenre>(userId, video.GenreId) != null;
            if (!hasAccess) return default(Video);

            return video;
        }

        public IEnumerable<Video> GetVideos(string userId, int albumId = 0)
        {
            var module = _db.Get<Album>(albumId);

            var hasAccess = _db.Get<UserGenre>(userId, module.GenreId) != null;
            if (!hasAccess) return default(IEnumerable<Video>);

            var videos = _db.Get<Video>().Where(v => v.AlbumId.Equals(albumId));

            return videos;
        }
    }
}
