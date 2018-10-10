using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Ui.Models.DTOModels;
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
            var courseDtoObjects = _mapper.Map<List<CourseDto>>(_db.GetCourses(_userId));

            var dashboardModel = new DashboardViewModel();
            dashboardModel.Courses = new List<List<CourseDto>>();

            var noOfRows = courseDtoObjects.Count <= 3 ? 1 : courseDtoObjects.Count / 3;
            for (var i = 0; i < noOfRows; i++)
                dashboardModel.Courses.Add(courseDtoObjects.Take(3).ToList());

            return View(dashboardModel);
        }

        [HttpGet]
        public IActionResult Genre(int id)
        {
            var course = _db.GetCourse(_userId, id);
            var mappedCourseDTOs = _mapper.Map<CourseDto>(course);
            var mappedInstructorDTO = _mapper.Map<InstructorDto>(course.Instructor);
            var mappedModuleDTOs = _mapper.Map<List<ModuleDto>>(course.Modules);

            for (var i = 0; i < mappedModuleDTOs.Count; i++)
            {
                mappedModuleDTOs[i].Downloads =
                    course.Modules[i].Downloads.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<DownloadDto>>(course.Modules[i].Downloads);

                mappedModuleDTOs[i].Videos =
                    course.Modules[i].Videos.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<VideoDto>>(course.Modules[i].Videos);
            }

            var courseModel = new CourseViewModel
            {
                Course = mappedCourseDTOs,
                Instructor = mappedInstructorDTO,
                Modules = mappedModuleDTOs
            };

            return View(courseModel);
        }

        [HttpGet]
        public IActionResult Band(int id)
        {
            var course = _db.GetCourse(_userId, id);
            var mappedCourseDTOs = _mapper.Map<CourseDto>(course);
            var mappedInstructorDTO = _mapper.Map<InstructorDto>(course.Instructor);
            var mappedModuleDTOs = _mapper.Map<List<ModuleDto>>(course.Modules);

            for (var i = 0; i < mappedModuleDTOs.Count; i++)
            {
                mappedModuleDTOs[i].Downloads =
                    course.Modules[i].Downloads.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<DownloadDto>>(course.Modules[i].Downloads);

                mappedModuleDTOs[i].Videos =
                    course.Modules[i].Videos.Count.Equals(0)
                        ? null
                        : _mapper.Map<List<VideoDto>>(course.Modules[i].Videos);
            }

            var courseModel = new CourseViewModel
            {
                Course = mappedCourseDTOs,
                Instructor = mappedInstructorDTO,
                Modules = mappedModuleDTOs
            };

            return View(courseModel);
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            var video = _db.GetVideo(_userId, id);
            var course = _db.GetCourse(_userId, video.CourseId);
            var mappedVideoDTO = _mapper.Map<VideoDto>(video);
            var mappedCourseDTO = _mapper.Map<CourseDto>(course);
            var mappedInstructorDTO =
                _mapper.Map<InstructorDto>(course.Instructor);

            // Create a LessonInfoDto object
            var videos = _db.GetVideos(_userId, video.ModuleId).ToList();
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
                Instructor = mappedInstructorDTO,
                Course = mappedCourseDTO,
                LessonInfo = new LessonInfoDto
                {
                    LessonNumber = index + 1,
                    NumberOfLessons = count,
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