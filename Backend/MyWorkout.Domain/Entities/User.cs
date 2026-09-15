using MyWorkout.Domain.Enums;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public Rols Rol { get; set; }

    public User(string name, string email, string password, Rols rol)
    {
        Name = name;
        Email = email;
        PasswordHash = password;
        Rol = rol;
    }

    List<workout> Workouts { get; set; }
}