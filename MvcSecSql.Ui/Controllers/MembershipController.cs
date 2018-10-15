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
//            _userId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _userId = "5ebbf9f5-e4ed-4250-bc2c-e03a961c6a45"; //todo: delete hardcoded
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var mappedGenres = _mapper.Map<List<GenreDto>>(_db.GetGenres(_userId));
            var dashboardModel = new DashboardViewModel {Genres = new List<List<GenreDto>>()};
            var rows = mappedGenres.Count <= 3 ? 1 : mappedGenres.Count / 3;
            for (var i = 0; i < rows; i++)
                dashboardModel.Genres.Add(mappedGenres.Take(3).ToList());
            return View(dashboardModel);
        }

        [HttpGet]
        public IActionResult Genre(int id = 1) //todo: no default value
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
            var mappedBand = _mapper.Map<BandDto>(band);
            var mappedBandMembers = _mapper.Map<List<BandMemberDto>>(band.BandMembers);
            var mappedAlbums = _mapper.Map<List<AlbumDto>>(band.Albums);
            var bandViewModel = new BandViewModel
            {
                Genre = _mapper.Map<GenreDto>(_db.GetGenre(_userId, id)),
                Band = mappedBand,
                BandMembers = mappedBandMembers,
                Albums = mappedAlbums
            };

            return View(bandViewModel);
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            var video = _db.GetVideo(_userId, id);
            var course = _db.GetGenre(_userId, video.GenreId);
            var mappedVideos = _mapper.Map<VideoDto>(video);
            var mappedGenres = _mapper.Map<GenreDto>(course);
            var mappedBand = _mapper.Map<BandDto>(course.Bands);

            // Create a VideoComingUpDto object
            var videos = _db.GetVideos(_userId, video.AlbumId).ToList();
            var count = videos.Count();
            var index = videos.IndexOf(video);
            var previous = videos.ElementAtOrDefault(index - 1);
            var previousId = previous?.Id ?? 0;
            var next = videos.ElementAtOrDefault(index + 1);
            var nextId = next?.Id ?? 0;
            var nextTitle = next == null ? string.Empty : next.Title;
            var nextThumb = next == null ? string.Empty : next.Thumbnail;

            var videoModel = new VideoViewModel
            {
                Video = mappedVideos,
                Band = mappedBand,
                Genre = mappedGenres,
                VideoComingUp = new VideoComingUpDto
                {
                    VideoNumber = index + 1,
                    NumberOfVideos = count,
                    NextVideoId = nextId,
                    PreviousVideoId = previousId,
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