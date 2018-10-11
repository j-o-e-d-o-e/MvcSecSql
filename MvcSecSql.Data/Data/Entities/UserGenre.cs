namespace MvcSecSql.Data.Data.Entities
{
    public class UserGenre
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}