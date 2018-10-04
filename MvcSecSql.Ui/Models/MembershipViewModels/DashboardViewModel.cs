using System.Collections.Generic;
using MvcSecSql.Ui.Models.DTOModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<CourseDto>> Courses { get; set; }
    }
}
