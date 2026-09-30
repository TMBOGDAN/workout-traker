using MyWorkout.Domain.Entities;
using MyWorkout.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net;

namespace MyWorkout.Infrastructure.Services;

public class WorkoutServices
{
    private readonly MyWorkoutDbContext _dbContext;

    public WorkoutServices(MyWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Workout CreateWorkout(string name, string description, int duration)
    {
        return new Workout(name, description)
        {
            Name = name,
            Notes = description
        };
    }

    public async Task<Workout?> UpdateWorkoutDetails(int workoutId, string name, string? notes)
    {
        var workout = await _dbContext.Workouts.FindAsync(workoutId);

        if (workout is null)
            return null;

        workout.Name = name;
        workout.Notes = notes;

        await _dbContext.SaveChangesAsync();

        return workout;
    }

    public async Task<bool> DeleteWorkout(int workoutId)
    {
        var workout = await _dbContext.Workouts.FindAsync(workoutId);

        if (workout is null)
            return false;

        _dbContext.Workouts.Remove(workout);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Workout?> ModofyWorkoutExercies(int workoutId, List<Excercise> exercises)
    {
        var workout = await _dbContext.Workouts.Include(w => w.Exercises)
    .FirstOrDefaultAsync(w => w.Id == workoutId);

        if (workout is null)
            return null;

        workout.Exercises.Clear();

        foreach (var exercise in exercises)
        {
            workout.Exercises.Add(exercise);
        }

        await _dbContext.SaveChangesAsync();
        return workout;
    }
}
