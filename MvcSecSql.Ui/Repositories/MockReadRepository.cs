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
            var res = MockData.UserGenres.Where(userGenre => userGenre.UserId.Equals(userId))
                .Join(MockData.Genres, userGenre => userGenre.GenreId, genre => genre.Id,
                    (userGenre, genre) => new {Genre = genre})
                .Select(genre => genre.Genre).ToList();

            foreach (var genre in res)
            {
                genre.Band = MockData.Bands.SingleOrDefault(band => band.Id.Equals(genre.BandId)); //todo: multiples
                genre.Albums = MockData.Albums.Where(album => album.GenreId.Equals(genre.Id)).ToList();
            }

            return res;
        }

        public Genre GetGenre(string userId, int genreId)
        {
            var res = MockData.UserGenres.Where(userGenre => userGenre.UserId.Equals(userId))
                .Join(MockData.Genres, userGenre => userGenre.GenreId, genre => genre.Id,
                    (userGenre, genre) => new {Genre = genre})
                .SingleOrDefault(s => s.Genre.Id.Equals(genreId))?.Genre;

            if (res == null) return null;
            res.Band = MockData.Bands.SingleOrDefault(band => band.Id.Equals(res.BandId)); //todo: multiples
            res.Albums = MockData.Albums.Where(album => album.GenreId.Equals(res.Id)).ToList();

            foreach (var album in res.Albums)
            {
                album.Infos = MockData.AlbumInfos.Where(albumInfo => albumInfo.AlbumId.Equals(album.Id)).ToList();
                album.Videos = MockData.Videos.Where(video => video.AlbumId.Equals(album.Id)).ToList();
            }

            return res;
        }

        public Video GetVideo(string userId, int videoId)
        {
            return MockData.Videos.Where(video => video.Id.Equals(videoId))
                .Join(MockData.UserGenres, video => video.GenreId, userGenre => userGenre.GenreId,
                    (video, userGenre) => new {Video = video, UserGenre = userGenre})
                .FirstOrDefault(vug => vug.UserGenre.UserId.Equals(userId))?.Video;
        }

        public IEnumerable<Video> GetVideos(string userId, int albumId = 0)
        {
            var res = MockData.Videos.Join(MockData.UserGenres, video => video.GenreId, userGenre => userGenre.GenreId,
                    (video, userGenre) => new {Video = video, UserGenre = userGenre})
                .Where(vug => vug.UserGenre.UserId.Equals(userId));

            return albumId.Equals(0)
                ? res.Select(video => video.Video)
                : res.Where(video => video.Video.AlbumId.Equals(albumId)).Select(video => video.Video);
        }
    }
}