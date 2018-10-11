using System.Collections.Generic;

namespace MvcSecSql.Ui.Models.DtoModels
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string AlbumTitle { get; set; }
        public int AlbumReleaseYear { get; set; }
        public List<VideoDto> Videos { get; set; }
        public List<AlbumInfoDto> Downloads { get; set; }
    }
}
