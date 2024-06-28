using Microsoft.EntityFrameworkCore;

namespace TaskManagerGUI.Data
{
    public class ServiceDatabaseContext : DbContext
    {
        public ServiceDatabaseContext(DbContextOptions<ServiceDatabaseContext> options) : base(options)
        {

        }

        public DbSet<Services> Services { get; set; }
    }
}
