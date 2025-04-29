using Microsoft.EntityFrameworkCore;
using TCPCommunication.Models;


namespace TCPCommunication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Call> Calls { get; set; }


    }
}
