namespace HealthCareApp.Utilities
{
    public interface IJwtUtility
    {
        string GenerateToken(string userId);
        string ValidateToken(string token);
    }
}