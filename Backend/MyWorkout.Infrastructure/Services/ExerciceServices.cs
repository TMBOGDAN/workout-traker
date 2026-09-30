
using MyWorkout.Domain.Entities;
using MyWorkout.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net;
public class ExerciseService
{
    private readonly MyWorkoutDbContext _dbContext;

    public ExerciseService(MyWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Excercise?> CreteExcercise(string name)
    {
        name = name.Trim().ToUpper();
        var excercise = await _dbContext.Excercises.FindAsync(name);
        if (excercise is not null)
        {
            return null;
        }
        var newExercise = new Excercise
        {
            Name = name
        };

        _dbContext.Excercises.Add(newExercise);
        await _dbContext.SaveChangesAsync();


        return newExercise;
    }

    public async Task<bool> ModifyExercise(Excercise excercise)
    {
        var ex = await _dbContext.Excercises.FirstOrDefaultAsync(w => w.Name == excercise.Name);
        if (ex is null)
            return false;

        ex.Name = excercise.Name;
        _dbContext.Excercises.Update(ex);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeteleExercise(string name)
    {
        var exercise = await _dbContext.Excercises.FirstOrDefaultAsync(w => w.Name == name);
        if (exercise is null)
            return false;

        _dbContext.Excercises.Remove(exercise);

        await _dbContext.SaveChangesAsync();



        return true;
    }
}
