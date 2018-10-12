using System.Collections.Generic;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.UI.Repositories
{
    public interface IReadRepository
    {
        IEnumerable<Genre> GetGenres(string userId);
        Genre GetGenre(string userId, int genreId);
        Video GetVideo(string userId, int videoId);
        IEnumerable<Video> GetVideos(string userId, int albumId = default(int));
    }
}
