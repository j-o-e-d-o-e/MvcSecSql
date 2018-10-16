using System.Collections.Generic;
using System.Linq;
using MvcSecSql.Data.Data;
using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.UI.Repositories
{
    public class MockReadRepository : IReadRepository
    {
        public IEnumerable<Genre> GetGenres(string userId)
        {
            return MockData.UserGenres.Where(userGenre => userGenre.UserId.Equals(userId))
                .Join(MockData.Genres, userGenre => userGenre.GenreId, genre => genre.Id,
                    (userGenre, genre) => new {Genre = genre})
                .Select(genre => genre.Genre).ToList();
        }

        public Genre GetGenre(string userId, int genreId)
        {
            var res = GetGenres(userId).SingleOrDefault(genre => genre.Id.Equals(genreId));

            if (res == null) return null;
            res.Bands = MockData.Bands.Where(band => band.GenreId.Equals(genreId)).ToList();
            return res;
        }

        public Band GetBand(int bandId)
        {
            var res = MockData.Bands.SingleOrDefault(band => band.Id.Equals(bandId));
            if (res == null) return null;
            res.Genre = MockData.Genres.SingleOrDefault(genre => genre.Id.Equals(res.GenreId));
            res.BandMembers = MockData.BandMembers.Where(bandMember => bandMember.BandId.Equals(bandId)).ToList();
            res.Albums = MockData.Albums.Where(album => album.BandId.Equals(bandId)).ToList();
            foreach (var album in res.Albums)
            {
                album.Infos = MockData.AlbumInfos.Where(albumInfo => albumInfo.AlbumId.Equals(album.Id)).ToList();
                album.Videos = MockData.Videos.Where(video => video.AlbumId.Equals(album.Id)).ToList();
            }

            return res;
        }

        public Video GetVideo(int videoId)
        {
            var res = MockData.Videos.SingleOrDefault(video => video.Id.Equals(videoId));
            if (res == null) return null;
            res.Album = MockData.Albums.SingleOrDefault(album => album.Id.Equals(res.AlbumId));
            return res;
        }

        public IEnumerable<Video> GetVideos(int videoId = 0)
        {
            return MockData.Videos.Where(video => video.AlbumId.Equals(videoId)).ToList();
        }
    }
}