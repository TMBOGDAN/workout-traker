using Microsoft.EntityFrameworkCore;
using MyWorkout.Infrastructure.Persistence;



var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(
        "Connection string-ul DefaultConnection nu există.");

builder.Services.AddDbContext<MyWorkoutDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
