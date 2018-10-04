using MvcSecSql.Ui.Models.DTOModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class VideoViewModel
    {
        public VideoDto Video { get; set; }
        public InstructorDto Instructor { get; set; }
        public CourseDto Course { get; set; }
        public LessonInfoDto LessonInfo { get; set; }
    }
}