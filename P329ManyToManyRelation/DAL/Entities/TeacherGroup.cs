namespace P329ManyToManyRelation.DAL.Entities
{
    public class TeacherGroup : Entity
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
