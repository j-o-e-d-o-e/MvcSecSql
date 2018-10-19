using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [MaxLength(255)]
        [Display(Name = "Marquee Image")]
        public string MarqueeImageUrl { get; set; }

        [MaxLength(80), Required]
        [Display (Name = "Genre Title")]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public IEnumerable<Band> Bands { get; set; }
    }
}