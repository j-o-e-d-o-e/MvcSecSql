using MvcSecSql.Data.Data.Entities;

namespace MvcSecSql.Admin.Models
{
    public class UseGenreModel
    {
        public string Email { get; set; }
        public string GenreTitle { get; set; }
        public UserGenre UserGenre { get; set; } = new UserGenre();
        public UserGenre UpdatedUserGenre { get; set; } = new UserGenre();
    }
}
