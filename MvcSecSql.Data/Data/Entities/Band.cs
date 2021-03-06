﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class Band
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        [Display(Name = "Band Name")]
        public string Name { get; set; }

        [MaxLength(80)]
        public string SubGenre { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(1024)]
        public string Image { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public List<BandMember> BandMembers { get; set; }

        public List<Album> Albums { get; set; }
    }
}