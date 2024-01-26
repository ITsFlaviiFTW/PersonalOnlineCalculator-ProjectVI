using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Data;

namespace PersonalOnlineCalculator.Services
{
    public class AuthenticationService
    {
          public async Task<User> GetUserById(int id)
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
    }
}
