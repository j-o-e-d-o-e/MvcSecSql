using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Admin.Models
{
    public class UserCourseModel
    {
        public string Email { get; set; }
        public string CourseTitle { get; set; }
        public UserGenre UserGenre { get; set; } = new UserGenre();
        public UserGenre UpdatedUserGenre { get; set; } = new UserGenre();
    }
}
