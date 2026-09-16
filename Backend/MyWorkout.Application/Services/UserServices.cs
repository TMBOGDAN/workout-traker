using MyWorkout.Domain.Entities;
using MyWorkout.Domain.Enums;

namespace Services
{
    public class UserServices
    {
        // Implementation of user services goes here
        void CreateUser(string name, string email, string password, Rols rol)
        {
            // Logic to create a new user
            User newUser = new User(name, email, password, rol);
            // Save newUser to the database or perform other actions
        }

        void UpdateUser(int userId, string name, string email, string password, Rols rol)
        {
            // Logic to update an existing user
            // Retrieve the user from the database using userId
            // Update the user's properties and save changes
        }

        void DeleteUser(int userId)
        {
            // Logic to delete a user
            // Retrieve the user from the database using userId and remove it
        }




    }
}