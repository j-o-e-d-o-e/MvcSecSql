using MvcSecSql.Ui.Models.DtoModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class VideoViewModel
    {
        public VideoDto Video { get; set; }
        public BandDto Band { get; set; }
        public GenreDto Genre { get; set; }
        public VideoComingUpDto VideoComingUp { get; set; }
    }
}