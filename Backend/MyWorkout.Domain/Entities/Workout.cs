namespace MyWorkout.Domain.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Excercise> Excercises { get; set; }

    }
}