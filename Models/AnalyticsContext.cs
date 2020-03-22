using Microsoft.EntityFrameworkCore;

namespace analytics.Models
{
    public class AnalyticsContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public AnalyticsContext(DbContextOptions options) : base(options) { }

        public DbSet<GenericSession> SessionGs {get;set;}
    }
}