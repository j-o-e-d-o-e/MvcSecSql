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
            return _db.Get<UserGenre>()
                .Where(userGenre => userGenre.UserId.Equals(userId))
                .Select(genre => genre.Genre);
        }

        public Genre GetGenre(string userId, int genreId)
        {
            var hasAccess = _db.Get<UserGenre>(userId, genreId) != null;
            if (!hasAccess) return default(Genre); //null

            var res = _db.Get<Genre>(genreId, true);
            return res;
        }

        public Band GetBand(int bandId)
        {
            var res = _db.Get<Band>(bandId, true);
            foreach (var album in res.Albums)
            {
                album.Videos = _db.Get<Video>().Where(video => video.AlbumId.Equals(album.Id)).ToList();
                album.Infos = _db.Get<AlbumInfo>().Where(info => info.AlbumId.Equals(album.Id)).ToList();
            }

            return res;
        }

        public Video GetVideo(int videoId)
        {
            var res = _db.Get<Video>(videoId);
            res.Album = _db.Get<Album>(res.AlbumId);
            return res;
        }

        public IEnumerable<Video> GetVideos(int videoId = default(int))
        {
            return _db.Get<Video>().Where(v => v.AlbumId.Equals(videoId));
        }
    }
}