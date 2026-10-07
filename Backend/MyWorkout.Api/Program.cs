using Microsoft.EntityFrameworkCore;
using MyWorkout.Domain.Entities;
using MyWorkout.Domain.Enums;
using MyWorkout.Infrastructure.Persistence;
using MyWorkout.Infrastructure.Services;



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

builder.Services.AddScoped<WorkoutServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyWorkoutDbContext>();

    await dbContext.Database.MigrateAsync();
    await SeedDevelopmentUsersAsync(dbContext);
}

app.UseCors("Frontend");
app.MapControllers();



app.MapGet("/", () => "Hello World!");

app.Run();

static async Task SeedDevelopmentUsersAsync(MyWorkoutDbContext dbContext)
{
    var developmentUsers = new[]
    {
        new { Name = "Admin", Email = "admin@gmail.com", Role = UserRole.Admin },
        new { Name = "User", Email = "user@gmail.com", Role = UserRole.User }
    };

    foreach (var developmentUser in developmentUsers)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == developmentUser.Email);

        if (user is null)
        {
            dbContext.Users.Add(new User(
                developmentUser.Name,
                developmentUser.Email,
                BCrypt.Net.BCrypt.HashPassword("123"),
                developmentUser.Role));

            continue;
        }

        user.Name = developmentUser.Name;
        user.Email = developmentUser.Email;
        user.NormalizedEmail = User.NormalizeEmail(developmentUser.Email);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("123");
        user.Role = developmentUser.Role;
    }

    await dbContext.SaveChangesAsync();
}
