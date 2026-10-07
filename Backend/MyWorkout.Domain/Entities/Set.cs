namespace MyWorkout.Domain.Entities
{
    public class Set
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public Set(int reps, decimal weight)
        {
            Reps = reps;
            Weight = weight;
        }

        public Set()
        {

        }

    }
}
