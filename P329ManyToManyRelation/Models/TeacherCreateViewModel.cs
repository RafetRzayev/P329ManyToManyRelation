using Microsoft.AspNetCore.Mvc.Rendering;

namespace P329ManyToManyRelation.Models
{
    public class TeacherCreateViewModel
    {
        public string Name { get; set; }
        public List<SelectListItem>? Groups { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
