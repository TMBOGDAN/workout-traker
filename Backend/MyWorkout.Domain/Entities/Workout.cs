namespace MyWorkout.Domain.Entities
{
    public class Workout
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<WorkoutExercise> WorkoutExercises { get; set; } = [];

        public Workout()
        {


        }

        public Workout(string name, string notes)
        {
            Name = name;
            Notes = notes;

        }

    }
}
