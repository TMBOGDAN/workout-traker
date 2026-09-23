using Microsoft.AspNetCore.Mvc;
using MyWorkout.Infrastructure.Persistence;

namespace MyWorkout.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly MyWorkoutDbContext _dbContext;

    public WorkoutController(MyWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    List<string> exercies = new List<string>
    {
        "Squat",
        "Bench Press",
        "Deadlift",
        "Pull-up"
    };


    [HttpGet]
    public IActionResult GetExercises()
    {
        return Ok(exercies);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetWorkouts(int id)
    {

        return Ok(exercies[id]);
    }



}
