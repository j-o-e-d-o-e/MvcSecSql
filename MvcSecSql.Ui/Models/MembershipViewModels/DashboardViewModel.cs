using System.Collections.Generic;
using MvcSecSql.Ui.Models.DtoModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<GenreDto>> Genres { get; set; }
    }
}
