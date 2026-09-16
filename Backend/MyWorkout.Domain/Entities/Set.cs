namespace MyWorkout.Domain.Entities
{
    public class Set
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }

        public Set(int reps, double weight)
        {
            Reps = reps;
            Weight = weight;
        }

        public Set()
        {

        }

    }
}