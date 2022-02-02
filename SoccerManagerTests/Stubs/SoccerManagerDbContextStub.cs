using Microsoft.EntityFrameworkCore;
using SoccerManager;
using System;

namespace SoccerManagerTests.Stubs
{
    internal class SoccerManagerDbContextStub : SoccerManagerDbContext, IDisposable
    {
        private static readonly string databaseName = "TestDatabase";

        public SoccerManagerDbContextStub(): base(
            new DbContextOptionsBuilder<SoccerManagerDbContext>()
            .UseInMemoryDatabase(databaseName).Options)
        {
        }

        override public void Dispose()
        {
            Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
