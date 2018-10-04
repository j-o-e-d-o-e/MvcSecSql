using System.Collections.Generic;

namespace MvcSecSql.Ui.Models.DTOModels
{
    public class ModuleDto
    {
        public int Id { get; set; }
        public string ModuleTitle { get; set; }
        public List<VideoDto> Videos { get; set; }
        public List<DownloadDto> Downloads { get; set; }
    }
}
