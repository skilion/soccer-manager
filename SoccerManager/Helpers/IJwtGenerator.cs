namespace SoccerManager.Helpers
{
    public interface IJwtGenerator
    {
        public string Generate(string email);
    }
}
