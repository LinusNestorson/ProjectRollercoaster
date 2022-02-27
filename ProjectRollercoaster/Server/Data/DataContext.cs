namespace ProjectRollercoaster.Server.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Handles database connection.
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Feed> Feeds { get; set; }

        public DbSet<Podcast> Podcasts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}