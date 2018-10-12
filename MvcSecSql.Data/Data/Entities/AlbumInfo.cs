using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class AlbumInfo
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Url { get; set; }

        public Album Album { get; set; }
        public int AlbumId { get; set; }
    }
}