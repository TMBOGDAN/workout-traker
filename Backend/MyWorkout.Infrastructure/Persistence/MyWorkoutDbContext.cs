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
    public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
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
            entity.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(user => user.Role)
                .IsRequired();
            entity.HasIndex(user => user.Email)
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
            entity.HasIndex(exercise => exercise.Name)
                .IsUnique();
            entity.HasOne(exercise => exercise.CreatedByUser)
                .WithMany(user => user.CreatedExercises)
                .HasForeignKey(exercise => exercise.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<WorkoutExercise>(entity =>
        {
            entity.HasIndex(workoutExercise => new
                {
                    workoutExercise.WorkoutId,
                    workoutExercise.ExerciseId
                })
                .IsUnique();
            entity.HasOne(workoutExercise => workoutExercise.Workout)
                .WithMany(workout => workout.WorkoutExercises)
                .HasForeignKey(workoutExercise => workoutExercise.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            entity.HasOne(workoutExercise => workoutExercise.Exercise)
                .WithMany(exercise => exercise.WorkoutExercises)
                .HasForeignKey(workoutExercise => workoutExercise.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.Property(set => set.Weight)
                .HasPrecision(10, 2);
            entity.HasOne(set => set.WorkoutExercise)
                .WithMany(workoutExercise => workoutExercise.Sets)
                .HasForeignKey(set => set.WorkoutExerciseId)
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
