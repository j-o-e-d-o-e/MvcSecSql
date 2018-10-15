using System.Collections.Generic;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Ui.Models.DtoModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class BandViewModel
    {
        public GenreDto Genre { get; set; }
        public BandDto Band { get; set; }
        public IEnumerable<AlbumDto> Albums { get; set; }
        public IEnumerable<BandMemberDto> BandMembers { get; set; }
    }
}