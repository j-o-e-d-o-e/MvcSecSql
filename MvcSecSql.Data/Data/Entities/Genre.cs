using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [MaxLength(255)]
        public string MarqueeImageUrl { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
        public List<Album> Albums { get; set; }
    }
}