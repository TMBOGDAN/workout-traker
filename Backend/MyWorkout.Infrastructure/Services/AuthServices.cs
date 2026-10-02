using MyWorkout.Domain.Entities;
using MyWorkout.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net;
using MyWorkout.Application.DTOs;

namespace MyWorkout.Infrastructure.Services;

public class AuthServices
{

    private readonly MyWorkoutDbContext _dbContext;

    public AuthServices(MyWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> LoginService(UserDto userDto)
    {



    }

}