namespace ProjectRollercoaster.Server.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Shared;

    public class DataContext : DbContext
    {
        public DbSet<UnitTest> UnitTests { get; set; }

        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}