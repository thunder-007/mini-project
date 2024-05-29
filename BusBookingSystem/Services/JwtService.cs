using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BusBookingSystem.Interfaces;

namespace BusBookingSystem.Services;

public class JwtService : IJwtService
{
    private readonly IUserService _userService;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService(IConfiguration configuration, IUserService userService)
    {
        _userService = userService;
        _secretKey = configuration["Jwt:SecretKey"]
            ?? throw new NullReferenceException("JWT Secret Key is not configured.");
        _issuer = configuration["Jwt:Issuer"]
            ?? throw new NullReferenceException("JWT Issuer is not configured.");
        _audience = configuration["Jwt:Audience"]
            ?? throw new NullReferenceException("JWT Audience is not configured.");
    }

    public string GenerateToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(6),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string ValidateToken(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        tokenHandler.ValidateToken(jwt, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        }, out SecurityToken validatedToken);

        return tokenHandler.ReadJwtToken(jwt).Subject;
    }

    public bool ValidateUser(string email, string password)
    {
        return _userService.ValidateUser(email, password);
    }
}
