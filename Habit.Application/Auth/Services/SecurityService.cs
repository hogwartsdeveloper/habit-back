using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Habit.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Habit.Application.Auth.Services;

public class SecurityService : ISecurityService
{
    private readonly IConfiguration _configuration;
    
    public SecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public TokenViewModel GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        var jwt = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: credentials);

        var refreshJwt = new JwtSecurityToken(expires: DateTime.Now.AddDays(15), signingCredentials: credentials);

        return new TokenViewModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
            RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshJwt)
        };
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParam = GetTokenValidationParameters(_configuration["Jwt:Key"]!);

        try
        {
            await tokenHandler.ValidateTokenAsync(token, validationParam);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private TokenValidationParameters GetTokenValidationParameters(string secretKey)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero,
        };
    }
}