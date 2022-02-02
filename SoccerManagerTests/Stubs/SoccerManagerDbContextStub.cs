using Microsoft.EntityFrameworkCore;
using SoccerManager;
using System;

namespace SoccerManagerTests.Stubs
{
    internal class SoccerManagerDbContextStub : SoccerManagerDbContext
    {
        public SoccerManagerDbContextStub(): base(
            new DbContextOptionsBuilder<SoccerManagerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options)
        {
        }
    }
}
