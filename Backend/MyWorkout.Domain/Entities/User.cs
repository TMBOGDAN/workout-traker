using MyWorkout.Domain.Enums;

namespace MyWorkout.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Rols Rol { get; set; }

    public List<Workout> Workouts { get; set; } = new();

    // Folosit de EF Core când citește datele
    private User()
    {
    }

    public User(string name, string email, string password, Rols rol)
    {
        Name = name;
        Email = email;
        PasswordHash = password;
        Rol = rol;
    }
}