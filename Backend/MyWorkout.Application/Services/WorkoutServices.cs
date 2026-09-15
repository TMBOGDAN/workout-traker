namespace Services
{

    class WorkoutServices
    {
        // Implementation of workout services goes here
        void CreateWorkout(string name, string description, int duration)
        {
            // Logic to create a new workout
            Workout newWorkout = new Workout(name, description, duration);
            // Save newWorkout to the database or perform other actions
        }

        void UpdateWorkout(int workoutId, string name, string description, int duration)
        {
            // Logic to update an existing workout
            // Retrieve the workout from the database using workoutId
            // Update the workout's properties and save changes
        }

        void DeleteWorkout(int workoutId)
        {
            // Logic to delete a workout
            // Retrieve the workout from the database using workoutId and remove it
        }
    }



}