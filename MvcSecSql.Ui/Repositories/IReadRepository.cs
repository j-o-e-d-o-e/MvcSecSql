using System.Collections.Generic;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.UI.Repositories
{
    public interface IReadRepository
    {
        IEnumerable<Genre> GetGenres(string userId);
        Genre GetGenre(string userId, int genreId);
        Band GetBand(int bandId);
        Video GetVideo(int videoId);
        IEnumerable<Video> GetVideos(int videoId = default(int));
    }
}
