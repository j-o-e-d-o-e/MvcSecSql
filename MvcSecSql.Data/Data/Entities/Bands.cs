using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class Bands
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Name { get; set; }

        [MaxLength(80)]
        public string BandSubGenre { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(1024)]
        public string BandImage { get; set; }

        public List<Genre> Genres { get; set; }
    }
}