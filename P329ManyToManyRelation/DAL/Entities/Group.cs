namespace P329ManyToManyRelation.DAL.Entities
{
    public class Group : Entity
    {
        public string Name { get; set; }

        public ICollection<TeacherGroup> TeacherGroups { get; set; }
    }
}
