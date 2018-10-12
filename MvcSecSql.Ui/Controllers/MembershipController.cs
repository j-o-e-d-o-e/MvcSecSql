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
            var courseDtoObjects = _mapper.Map<List<GenreDto>>(_db.GetGenres(_userId));

            var dashboardModel = new DashboardViewModel();
            dashboardModel.Genres = new List<List<GenreDto>>();

            var noOfRows = courseDtoObjects.Count <= 3 ? 1 : courseDtoObjects.Count / 3;
            for (var i = 0; i < noOfRows; i++)
                dashboardModel.Genres.Add(courseDtoObjects.Take(3).ToList());

            return View(dashboardModel);
        }

        [HttpGet]
        public IActionResult Genre(int id)
        {
            var course = _db.GetGenre(_userId, id);
            var mappedCourseDTOs = _mapper.Map<GenreDto>(course);
//            var mappedInstructorDTO = _mapper.Map<BandDto>(course.Bands);
            var mappedModuleDTOs = _mapper.Map<List<AlbumDto>>(course.Albums);

            for (var i = 0; i < mappedModuleDTOs.Count; i++)
            {
                mappedModuleDTOs[i].Infos =
                    course.Albums[i].Infos.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<AlbumInfoDto>>(course.Albums[i].Infos);

                mappedModuleDTOs[i].Videos =
                    course.Albums[i].Videos.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<VideoDto>>(course.Albums[i].Videos);
            }

            var courseModel = new GenreViewModel
            {
                Genre = mappedCourseDTOs,
//                Band = mappedInstructorDTO,
                Albums = mappedModuleDTOs
            };

            return View(courseModel);
        }

        [HttpGet]
        public IActionResult Band(int id)
        {
            var course = _db.GetGenre(_userId, id);
            var mappedCourseDTOs = _mapper.Map<GenreDto>(course);
            var mappedInstructorDTO = _mapper.Map<BandDto>(course.Bands);
            var mappedModuleDTOs = _mapper.Map<List<AlbumDto>>(course.Albums);

            for (var i = 0; i < mappedModuleDTOs.Count; i++)
            {
                mappedModuleDTOs[i].Infos =
                    course.Albums[i].Infos.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<AlbumInfoDto>>(course.Albums[i].Infos);

                mappedModuleDTOs[i].Videos =
                    course.Albums[i].Videos.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<VideoDto>>(course.Albums[i].Videos);
            }

            var courseModel = new GenreViewModel
            {
                Genre = mappedCourseDTOs,
                Band = mappedInstructorDTO,
                Albums = mappedModuleDTOs
            };

            return View(courseModel);
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            var video = _db.GetVideo(_userId, id);
            var course = _db.GetGenre(_userId, video.GenreId);
            var mappedVideoDTO = _mapper.Map<VideoDto>(video);
            var mappedCourseDTO = _mapper.Map<GenreDto>(course);
            var mappedInstructorDTO =
                _mapper.Map<BandDto>(course.Bands);

            // Create a VideoComingUpDto object
            var videos = _db.GetVideos(_userId, video.AlbumId).ToList();
            var count = videos.Count();
            var index = videos.IndexOf(video);
            var previous = videos.ElementAtOrDefault(index - 1);
            var previousId = previous == null ? 0 : previous.Id;
            var next = videos.ElementAtOrDefault(index + 1);
            var nextId = next == null ? 0 : next.Id;
            var nextTitle = next == null ? string.Empty : next.Title;
            var nextThumb = next == null ? string.Empty : next.Thumbnail;

            var videoModel = new VideoViewModel
            {
                Video = mappedVideoDTO,
                Band = mappedInstructorDTO,
                Genre = mappedCourseDTO,
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