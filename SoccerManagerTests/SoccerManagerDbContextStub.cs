using Microsoft.EntityFrameworkCore;
using SoccerManager;

namespace SoccerManagerTests
{
    internal class SoccerManagerDbContextStub : SoccerManagerDbContext
    {
        private static readonly string databaseName = "TestDatabase";

        public SoccerManagerDbContextStub(): base(
            new DbContextOptionsBuilder<SoccerManagerDbContext>()
            .UseInMemoryDatabase(databaseName).Options)
        {
        }
    }
}
