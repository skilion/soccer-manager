using SoccerManager.Helpers;

namespace SoccerManagerTests.Stubs
{
    internal class JwtGeneratorStub : IJwtGenerator
    {
        public string Generate(string email)
        {
            return "TestToken";
        }
    }
}
