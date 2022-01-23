namespace ProjectRollercoaster.Server.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Shared;

    public class DataContext : DbContext
    {
        //public DbSet<UnitTest> UnitTests { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Feed> Feeds { get; set; }

        //public DbSet<UserFeed> UserFeeds { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}