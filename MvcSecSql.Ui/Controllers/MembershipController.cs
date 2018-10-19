using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Ui.Models.DtoModels;
using MvcSecSql.UI.Models.MembershipViewModels;
using MvcSecSql.UI.Repositories;

namespace MvcSecSql.UI.Controllers
{
    public class MembershipController : Controller
    {
        private readonly string _userId;
        private readonly IMapper _mapper;
        private readonly IReadRepository _db;

        public MembershipController(IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager, IMapper mapper, IReadRepository db)
        {
            _userId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var mappedGenres = _mapper.Map<List<GenreDto>>(_db.GetGenres(_userId));
            var rows = mappedGenres.Count <= 3 ? 1 : mappedGenres.Count / 3;
            var dashboardModel = new DashboardViewModel { Genres = new List<List<GenreDto>>() };
            for (var i = 0; i < rows; i++)
                dashboardModel.Genres.Add(mappedGenres.Take(3).ToList());
            return View(dashboardModel);
        }

        [HttpGet]
        public IActionResult Genre(int id = 1)
        {
            var genre = _db.GetGenre(_userId, id);
            var genreViewModel = new GenreViewModel
            {
                Genre = _mapper.Map<GenreDto>(genre),
                Bands = _mapper.Map<List<BandDto>>(genre.Bands),
            };

            return View(genreViewModel);
        }

        [HttpGet]
        public IActionResult Band(int id)
        {
            var band = _db.GetBand(id);
            var bandViewModel = new BandViewModel
            {
                Genre = _mapper.Map<GenreDto>(band.Genre),
                Band = _mapper.Map<BandDto>(band),
                BandMembers = _mapper.Map<List<BandMemberDto>>(band.BandMembers),
                Albums = _mapper.Map<List<AlbumDto>>(band.Albums),
            };

            return View(bandViewModel);
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            var video = _db.GetVideo(id);

            // Create a VideoComingUpDto object
            var videos = _db.GetVideos(video.AlbumId).ToList();
            var index = videos.IndexOf(video);
            var prevVideo = videos.ElementAtOrDefault(index - 1);
            var nextVideo = videos.ElementAtOrDefault(index + 1);
            var nextTitle = nextVideo == null ? string.Empty : nextVideo.Title;
            var nextThumb = nextVideo == null ? string.Empty : nextVideo.Thumbnail;

            var videoModel = new VideoViewModel
            {
                Genre = _mapper.Map<GenreDto>(_db.GetGenre(_userId, video.Album.GenreId)),
                Band = _mapper.Map<BandDto>(_db.GetBand(video.Album.BandId)),
                Video = _mapper.Map<VideoDto>(video),
                VideoComingUp = new VideoComingUpDto
                {
                    VideoNumber = index + 1,
                    NumberOfVideos = videos.Count(),
                    NextVideoId = nextVideo?.Id ?? 0,
                    PreviousVideoId = prevVideo?.Id ?? 0,
                    CurrentVideoTitle = video.Title,
                    CurrentVideoThumbnail = video.Thumbnail,
                    NextVideoTitle = nextTitle,
                    NextVideoThumbnail = nextThumb
                }
            };

            return View(videoModel);
        }
    }
}