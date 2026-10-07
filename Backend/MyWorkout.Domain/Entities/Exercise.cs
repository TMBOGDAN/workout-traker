namespace MyWorkout.Domain.Entities
{

    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public List<WorkoutExercise> WorkoutExercises { get; set; } = [];

        public Exercise(string name)
        {
            Name = name;
        }

        public Exercise()
        {

        }

    }


}
