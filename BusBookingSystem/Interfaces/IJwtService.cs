namespace BusBookingSystem.Interfaces;

public interface IJwtService
{
    string GenerateToken(string username, string role);
    string ValidateToken(string jwt);
    bool ValidateUser(string email, string password);
}
