using Microsoft.EntityFrameworkCore;

namespace TaskManagerGUI.Data
{
    public class SelectCompanyContext : DbContext
    {
        public SelectCompanyContext(DbContextOptions<SelectCompanyContext> options) : base(options)
        {

        }

        // Table Name
        public DbSet<SelectCompany> Companies { get; set; }
    }
}
