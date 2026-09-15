namespace MyWorkout.Domain.Entities
{

    class Excercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Set> Sets { get; set; }

    }


}
