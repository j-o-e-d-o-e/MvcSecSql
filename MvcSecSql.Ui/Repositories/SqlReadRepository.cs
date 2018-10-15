using System.Collections.Generic;
using System.Linq;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;

namespace MvcSecSql.UI.Repositories
{
    public class SqlReadRepository : IReadRepository
    {
        private readonly IDbReadService _db;

        public SqlReadRepository(IDbReadService db)
        {
            _db = db;
        }

        public IEnumerable<Genre> GetGenres(string userId)
        {
            return _db.GetWithIncludes<UserGenre>()
                .Where(userGenre => userGenre.UserId.Equals(userId))
                .Select(genre => genre.Genre);
        }

        public Genre GetGenre(string userId, int genreId)
        {
            var hasAccess = _db.Get<UserGenre>(userId, genreId) != null;
            if (!hasAccess) return default(Genre);

            var genre = _db.Get<Genre>(genreId, true);

//            foreach (var album in genre.Albums)
//            {
//                album.Infos = _db.Get<AlbumInfo>().Where(albumInfo =>
//                    albumInfo.AlbumId.Equals(album.VideoId)).ToList();
//
//                album.Videos = _db.Get<Video>().Where(video =>
//                    video.AlbumId.Equals(album.VideoId)).ToList();
//            }

            return genre;
        }

        public Band GetBand(int bandId)
        {
            throw new System.NotImplementedException();
        }

        public Video GetVideo(string userId, int videoId)
        {
            var video = _db.Get<Video>(videoId);

//            var hasAccess = _db.Get<UserGenre>(userId, video.BandId) != null;
//            if (!hasAccess) return default(Video);

            return video;
        }

        public IEnumerable<Video> GetVideos(int albumId = default(int))
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Video> GetVideos(string userId, int albumId = 0)
        {
            var module = _db.Get<Album>(albumId);

            var hasAccess = _db.Get<UserGenre>(userId, module.BandId) != null;
            if (!hasAccess) return default(IEnumerable<Video>);

            var videos = _db.Get<Video>().Where(v => v.AlbumId.Equals(albumId));

            return videos;
        }
    }
}