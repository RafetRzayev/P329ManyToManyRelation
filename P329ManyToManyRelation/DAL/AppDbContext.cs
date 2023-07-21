using Microsoft.EntityFrameworkCore;
using P329ManyToManyRelation.DAL.Entities;

namespace P329ManyToManyRelation.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
               
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TeacherGroup> TeachersGroup { get; set;}
    }
}
