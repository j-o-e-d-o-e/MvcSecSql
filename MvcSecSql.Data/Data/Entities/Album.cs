﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80)]
        [Required]
        [Display(Name = "Album Title")]
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<Video> Videos { get; set; }
        public List<AlbumInfo> Infos { get; set; }
    }
}