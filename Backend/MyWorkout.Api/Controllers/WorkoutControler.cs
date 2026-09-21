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


    [HttpGet]
    public IActionResult SayHello()
    {
        return Ok("Hello from WorkoutController!");
    }

    [HttpGet]
    public IActionResult GetWorkouts()
    {
        return Ok("Lista antrenamentelor");
    }


}