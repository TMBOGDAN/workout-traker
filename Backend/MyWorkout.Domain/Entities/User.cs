using MyWorkout.Domain.Enums;



namespace MyWorkout.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public List<Workout> Workouts { get; set; } = new();

    public List<RefreshToken> RefreshTokens { get; set; } = new();

    private User()
    {
    }

    public User(string name, string email, string passwordHash, UserRole role)
    {
        Name = name;
        Email = email;
        NormalizedEmail = NormalizeEmail(email);
        PasswordHash = passwordHash;
        Role = role;
    }

    public User(User user_p)
    {
        this.Email = user_p.Email;
        this.Name = user_p.Name;
        this.NormalizedEmail = NormalizeEmail(user_p.Email);
        this.PasswordHash = user_p.PasswordHash;
        this.Role = user_p.Role;
    }

    public static string NormalizeEmail(string email) => email.Trim().ToUpperInvariant();
}
