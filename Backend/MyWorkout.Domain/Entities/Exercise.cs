namespace MyWorkout.Domain.Entities
{

    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;
        public List<Set> Sets { get; set; } = new List<Set>();

        public Exercise(string name, List<Set> sets)
        {
            Name = name;
            Sets = sets;
        }

        public Exercise()
        {

        }

    }


}
