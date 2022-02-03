using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager
{
    public class SoccerManagerDbContext: DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Transfer> Transfers => Set<Transfer>();

        public SoccerManagerDbContext(DbContextOptions<SoccerManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Transfer>().ToTable("Transfer");
        }
    }
}
