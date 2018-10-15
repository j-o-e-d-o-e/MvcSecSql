using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSecSql.Ui.Models.DtoModels
{
    public class GenreBandDto
    {
        [DisplayName("Band VideoId")]
        public string GenreId { get; set; }

        [DisplayName("Bands VideoId")]
        public int BandId { get; set; }

        [DisplayName("Band Title")]
        public string GenreTitle { get; set; }

        [DisplayName("Bands Name")]
        public string BandName { get; set; }
    }
}