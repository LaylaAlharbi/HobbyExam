using Microsoft.EntityFrameworkCore;
namespace HobbyExam.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Hobby> hobbies { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}