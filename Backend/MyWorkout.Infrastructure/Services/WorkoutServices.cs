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

    public async Task<Workout?> ModifyWorkoutExercises(int workoutId, List<int> exerciseIds)
    {
        var workout = await _dbContext.Workouts
            .Include(workout => workout.WorkoutExercises)
            .FirstOrDefaultAsync(workout => workout.Id == workoutId);

        if (workout is null)
            return null;

        var distinctExerciseIds = exerciseIds.Distinct().ToList();
        var existingExerciseIds = await _dbContext.Exercises
            .Where(exercise => distinctExerciseIds.Contains(exercise.Id))
            .Select(exercise => exercise.Id)
            .ToListAsync();

        if (existingExerciseIds.Count != distinctExerciseIds.Count)
        {
            return null;
        }

        var workoutExercisesToRemove = workout.WorkoutExercises
            .Where(workoutExercise => !distinctExerciseIds.Contains(workoutExercise.ExerciseId))
            .ToList();

        _dbContext.WorkoutExercises.RemoveRange(workoutExercisesToRemove);

        var currentExerciseIds = workout.WorkoutExercises
            .Select(workoutExercise => workoutExercise.ExerciseId)
            .ToHashSet();

        foreach (var exerciseId in distinctExerciseIds.Where(id => !currentExerciseIds.Contains(id)))
        {
            workout.WorkoutExercises.Add(new WorkoutExercise
            {
                ExerciseId = exerciseId
            });
        }

        await _dbContext.SaveChangesAsync();
        return workout;
    }
}
