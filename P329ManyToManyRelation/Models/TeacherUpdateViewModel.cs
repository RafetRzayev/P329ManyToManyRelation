using Microsoft.AspNetCore.Mvc.Rendering;

namespace P329ManyToManyRelation.Models
{
    public class TeacherUpdateViewModel
    {
        public string Name { get; set; }
        public List<SelectListItem>? RemovedGroups { get; set; }
        public List<int> RemovedGroupIds { get; set; }
    }
}
