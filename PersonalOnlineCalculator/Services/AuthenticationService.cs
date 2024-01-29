using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Data;
using System.Threading.Tasks;
using System.Linq; // Needed for LINQ queries
using System.Security.Cryptography; // Needed for hashing passwords
using System.Text; // Needed for encoding strings to bytes


namespace PersonalOnlineCalculator.Services
{
    public class AuthenticationService
    {
          public User GetUserById(int id)
        {
            return Database.GetUser(id);
        }

        public void UpdateUserProfile(User updatedUser)
        {
            Database.UpdateUser(updatedUser);
        }

        public void UpdateUserPassword(int id, string newPassword)
        {
            Database.UpdatePassword(id, newPassword);
        }

        public async Task<User> RegisterUser(User newUser)
        {
            // Check if user already exists
            var existingUser = Database.GetUser(newUser.Email);
            if (existingUser != null)
            {
                // User already exists
                throw new System.Exception("A user with this email already exists.");
            }

            // Hash the password before saving to the database
            newUser.PasswordHash = HashPassword(newUser.PasswordHash);

            // Add the new user to the database
            Database.AddUser(newUser); 
            return newUser;
        }

        public async Task<User> LoginUser(string username, string password)
        {
            // Retrieve user by username
            var user = Database.GetUser(username); 
            if (user == null)
            {
                // User not found
                throw new System.Exception("User not found.");
            }

            // Verify the password
            var hashedPassword = HashPassword(password);
            if (user.PasswordHash != hashedPassword)
            {
                // Password does not match
                throw new System.Exception("Password is incorrect.");
            }

            return user;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Get the hashed string
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
