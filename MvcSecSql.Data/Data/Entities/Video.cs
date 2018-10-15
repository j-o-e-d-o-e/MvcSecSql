﻿using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class Video
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80)]
        [Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public int Duration { get; set; }

        [MaxLength(1024)]
        public string Thumbnail { get; set; }

        [MaxLength(1024)]
        public string Url { get; set; }

        public int Position { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}