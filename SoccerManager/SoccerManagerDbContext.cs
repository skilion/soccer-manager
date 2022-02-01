using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager
{
    public class SoccerManagerDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

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
