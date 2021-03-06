﻿using System.ComponentModel;

namespace MvcSecSql.Ui.Models.DtoModels
{
    public class UserGenreDto
    {
        [DisplayName("User VideoId")]
        public string UserId { get; set; }
        [DisplayName("Bands VideoId")]
        public int GenreId { get; set; }
        [DisplayName("Email")]
        public string UserEmail { get; set; }
        [DisplayName("Title")]
        public string GenreTitle { get; set; }
    }
}
