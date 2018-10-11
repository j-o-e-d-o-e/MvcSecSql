namespace MvcSecSql.Ui.Models.DtoModels
{
    public class VideoComingUpDto
    {
        public int VideoNumber { get; set; }
        public int NumberOfVideos { get; set; }
        public int PreviousVideoId { get; set; }
        public int NextVideoId { get; set; }
        public string CurrentVideoTitle { get; set; }
        public string CurrentVideoThumbnail { get; set; }
        public string NextVideoTitle { get; set; }
        public string NextVideoThumbnail { get; set; }
    }
}
