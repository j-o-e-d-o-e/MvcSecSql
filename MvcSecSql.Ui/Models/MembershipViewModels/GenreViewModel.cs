using System.Collections.Generic;
using MvcSecSql.Ui.Models.DtoModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class GenreViewModel
    {
        public GenreDto Genre { get; set; }
        public BandDto Band { get; set; }
        public IEnumerable<AlbumDto> Albums { get; set; }
    }
}