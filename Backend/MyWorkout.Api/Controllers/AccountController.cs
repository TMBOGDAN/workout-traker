using Microsoft.AspNetCore.Mvc;
using MyWorkout.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Application.DTOs;
using MyWorkout.Domain.Entities;
using MyWorkout.Domain.Enums;
namespace MyWorkout.Api.Controllers;


[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    private readonly MyWorkoutDbContext _dbContext;

    public AccountController(MyWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            return Unauthorized("Email sau parolă incorectă.");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return Unauthorized("Email sau parolă incorectă.");
        }
        return StatusCode(200, "Approved");



    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountDto accountDto)
    {
        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == accountDto.Email);

        if (existingUser != null)
        {
            return Conflict("User already exists.");
        }

        var user = new User(
            accountDto.Username,
            accountDto.Email,
            BCrypt.Net.BCrypt.HashPassword(accountDto.Password),
            Rols.User);

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return Ok("Account created.");
    }

}

