using MyWorkout.Application.DTOs;
using MyWorkout.Domain.Entities;
using MyWorkout.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net;
using MyWorkout.Infrastructure.Persistence;
namespace MyWorkout.Infrastructure.Services
{

    public class UserServices
    {
        private readonly MyWorkoutDbContext _dbContext;

        public UserServices(MyWorkoutDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<User?> CreateUser(string name, string email, string password, Rols rol)
        {
            User newUser = new User(name, email, password, rol);
            var user = _dbContext.Users.FirstOrDefault(w => w.Email == newUser.Email);
            if (user is null)
            {
                return null;
            }

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> UpdateUser(UserDto user_p)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(w => w.Email == user_p.Email);

            return true;
        }

        void DeleteUser(UserDto user_p)
        {

        }
    }


}

