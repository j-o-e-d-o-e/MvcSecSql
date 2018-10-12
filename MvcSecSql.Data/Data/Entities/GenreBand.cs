namespace MvcSecSql.Data.Data.Entities
{
    public class GenreBand
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
    }
}