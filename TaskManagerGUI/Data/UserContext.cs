using Microsoft.EntityFrameworkCore;

namespace TaskManagerGUI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<Logins> Users { get; set; }
    }
}
