using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager
{
    public class SoccerManagerDbContext: DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<Player> Player { get; set; }
        DbSet<Transfer> Transfers { get; set; }

        public SoccerManagerDbContext(DbContextOptions<SoccerManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Transfer>().ToTable("Transfer");
        }
    }
}
