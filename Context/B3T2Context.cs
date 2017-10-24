using Microsoft.EntityFrameworkCore;

 
namespace B3T2.Models
{
    public class B3T2Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public B3T2Context(DbContextOptions<B3T2Context> options) : base(options) { }
        public DbSet<User> user { get; set; }
        public DbSet<Ideas> ideas { get; set; }
        public DbSet<Liked> liked { get; set; }
     
    }
}
