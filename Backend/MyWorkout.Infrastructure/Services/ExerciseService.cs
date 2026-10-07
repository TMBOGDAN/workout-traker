using Microsoft.EntityFrameworkCore;
using MyWorkout.Domain.Entities;
using MyWorkout.Infrastructure.Persistence;

namespace MyWorkout.Infrastructure.Services;

public class ExerciseService
{
    private readonly MyWorkoutDbContext _dbContext;

    public ExerciseService(MyWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Exercise?> CreateExercise(int workoutId, string name)
    {
        name = name.Trim().ToUpperInvariant();
        var exercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(existingExercise =>
                existingExercise.WorkoutId == workoutId && existingExercise.Name == name);

        if (exercise is not null)
        {
            return null;
        }

        var newExercise = new Exercise
        {
            Name = name,
            WorkoutId = workoutId
        };

        _dbContext.Exercises.Add(newExercise);
        await _dbContext.SaveChangesAsync();

        return newExercise;
    }

    public async Task<bool> ModifyExercise(Exercise exercise)
    {
        var existingExercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(item => item.Id == exercise.Id);

        if (existingExercise is null)
        {
            return false;
        }

        existingExercise.Name = exercise.Name;
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteExercise(int exerciseId)
    {
        var exercise = await _dbContext.Exercises.FindAsync(exerciseId);

        if (exercise is null)
        {
            return false;
        }

        _dbContext.Exercises.Remove(exercise);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
