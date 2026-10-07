using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyWorkout.Application.DTOs;
using MyWorkout.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyWorkout.Domain.Entities;
using System.Security.Cryptography;
using MyWorkout.Domain.Enums;
namespace MyWorkout.Infrastructure.Services;

public class AuthServices
{
    private readonly MyWorkoutDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthServices(MyWorkoutDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<AccountResponseDto?> LoginService(LoginDto loginDto)
    {


        var email = loginDto.Email.Trim();
        var user = await _dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            return null;
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

        if (!passwordIsValid)
        {
            return null;
        }

        var token = GenerateJwt(user);
        var refreshToken = GenerateRefreshToken();
        var now = DateTime.UtcNow;

        _dbContext.RefreshTokens.Add(new RefreshToken
        {
            TokenHash = HashRefreshToken(refreshToken),
            UserId = user.Id,
            CreatedAtUtc = now,
            ExpiresAtUtc = now.AddDays(30)
        });
        await _dbContext.SaveChangesAsync();

        return new AccountResponseDto
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            AccessTokenExpiresAtUtc = now.AddHours(2)
        };
    }

    public async Task<bool> RegisterServices(AccountDto accountDto)
    {
        var email = accountDto.Email.Trim();

        var userExists = await _dbContext.Users
            .AnyAsync(user => user.Email == email);

        if (userExists)
        {
            return false;
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(
            accountDto.Password);

        var newUser = new User(accountDto.Username.Trim(), email, passwordHash, UserRole.User);

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return true;
    }


    private string GenerateJwt(Domain.Entities.User user)
    {
        var jwtKey = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key nu este configurat.");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    private static string HashRefreshToken(string refreshToken)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(refreshToken);
        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToHexString(hashBytes);
    }

}
