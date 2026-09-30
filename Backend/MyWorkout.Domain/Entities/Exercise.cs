namespace MyWorkout.Domain.Entities
{

    public class Excercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Set> Sets { get; set; } = new List<Set>();

        public Excercise(string name, List<Set> sets)
        {
            Name = name;
            Sets = sets;
        }

        public Excercise()
        {

        }

    }


}
