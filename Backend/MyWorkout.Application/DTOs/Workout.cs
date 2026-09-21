namespace MyWorkout.Application.DTOs
{

    public class WOrkoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        List<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();
    }

    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int Repetitions { get; set; }
        public int Sets { get; set; }
    }



}