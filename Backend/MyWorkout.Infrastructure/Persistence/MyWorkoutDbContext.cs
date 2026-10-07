using Microsoft.EntityFrameworkCore;
using MyWorkout.Domain.Entities;

namespace MyWorkout.Infrastructure.Persistence;

public class MyWorkoutDbContext : DbContext
{
    public MyWorkoutDbContext(DbContextOptions<MyWorkoutDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Set> Sets => Set<Set>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(254);
            entity.Property(user => user.NormalizedEmail)
                .IsRequired()
                .HasMaxLength(254);
            entity.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(user => user.Role)
                .IsRequired();
            entity.HasIndex(user => user.NormalizedEmail)
                .IsUnique();
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.Property(workout => workout.Name)
                .IsRequired()
                .HasMaxLength(120);
            entity.Property(workout => workout.Notes)
                .HasMaxLength(1000);
            entity.HasOne(workout => workout.User)
                .WithMany(user => user.Workouts)
                .HasForeignKey(workout => workout.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(exercise => exercise.Name)
                .IsRequired()
                .HasMaxLength(120);
            entity.HasOne(exercise => exercise.Workout)
                .WithMany(workout => workout.Exercises)
                .HasForeignKey(exercise => exercise.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.Property(set => set.Weight)
                .HasPrecision(10, 2);
            entity.HasOne(set => set.Exercise)
                .WithMany(exercise => exercise.Sets)
                .HasForeignKey(set => set.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.Property(refreshToken => refreshToken.TokenHash)
                .IsRequired()
                .HasMaxLength(64);
            entity.HasIndex(refreshToken => refreshToken.TokenHash)
                .IsUnique();
            entity.HasOne(refreshToken => refreshToken.User)
                .WithMany(user => user.RefreshTokens)
                .HasForeignKey(refreshToken => refreshToken.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });
    }
}
