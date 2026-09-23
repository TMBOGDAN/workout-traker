using Microsoft.EntityFrameworkCore;
using MyWorkout.Infrastructure.Persistence;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(
        "Connection string-ul DefaultConnection nu există.");

builder.Services.AddDbContext<MyWorkoutDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();
app.UseCors("Frontend");
app.MapControllers();



app.MapGet("/", () => "Hello World!");

app.Run();
