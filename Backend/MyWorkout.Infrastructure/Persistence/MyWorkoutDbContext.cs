using Microsoft.EntityFrameworkCore;
using MyWorkout.Domain.Entities;

namespace MyWorkout.Infrastructure.Persistence;

public class MyWorkoutDbContext : DbContext
{
    public MyWorkoutDbContext(DbContextOptions<MyWorkoutDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Excercise> Excercises => Set<Excercise>();
    public DbSet<Set> Sets => Set<Set>();


}