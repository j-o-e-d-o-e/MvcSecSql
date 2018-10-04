using System.Collections.Generic;
using MvcSecSql.Ui.Models.DTOModels;

namespace MvcSecSql.UI.Models.MembershipViewModels
{
    public class CourseViewModel
    {
        public CourseDto Course { get; set; }
        public InstructorDto Instructor { get; set; }
        public IEnumerable<ModuleDto> Modules { get; set; }
    }
}