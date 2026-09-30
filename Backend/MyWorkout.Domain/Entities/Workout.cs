namespace MyWorkout.Domain.Entities
{
    public class Workout
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public int UserId { get; set; }

        public List<Excercise> Exercises { get; set; } = [];

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