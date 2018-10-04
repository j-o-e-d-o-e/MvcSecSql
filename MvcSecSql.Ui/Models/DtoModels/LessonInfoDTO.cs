﻿namespace MvcSecSql.Ui.Models.DTOModels
{
    public class LessonInfoDto
    {
        public int LessonNumber { get; set; }
        public int NumberOfLessons { get; set; }
        public int PreviousVideoId { get; set; }
        public int NextVideoId { get; set; }
        public string CurrentVideoTitle { get; set; }
        public string CurrentVideoThumbnail { get; set; }
        public string NextVideoTitle { get; set; }
        public string NextVideoThumbnail { get; set; }
    }
}
