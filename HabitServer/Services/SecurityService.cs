using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HabitServer.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace HabitServer.Services;

public class SecurityService : ISecurityService
{
    private readonly IConfiguration _configuration;

    public SecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims: null,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}