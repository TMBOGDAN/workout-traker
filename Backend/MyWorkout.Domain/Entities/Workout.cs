namespace MyWorkout.Domain.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Excercise> Excercises { get; set; } = new List<Excercise>();

        public Workout()
        {

        }
        public Workout(string name, List<Excercise> excercises)
        {
            Name = name;
            Excercises = excercises;
        }



    }
}