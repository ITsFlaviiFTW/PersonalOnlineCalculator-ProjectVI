using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace PersonalOnlineCalculator.Services
{
    public class AuthenticationService
    {
        private const string UserFilePath = "Users.json";

        public async Task<User> RegisterUser(User newUser)
        {
            List<User> users = await ReadUsersFromFile();
            if (users.Any(u => u.Username == newUser.Username || u.Email == newUser.Email))
            {
                throw new Exception("User already exists.");
            }
            Database.AddUser(newUser.Username, newUser.Email, newUser.PasswordHash);
            return newUser;
        }

        public async Task<User> LoginUser(string username, string passwordHash)
        {
            List<User> users = await ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
            if (user == null)
            {
                throw new Exception("Invalid username or password.");
            }
            return user;
        }

        private async Task<List<User>> ReadUsersFromFile()
        {
            if (!File.Exists(UserFilePath))
            {
                return new List<User>();
            }

            using (var stream = File.OpenRead(UserFilePath))
            {
                return await JsonSerializer.DeserializeAsync<List<User>>(stream) ?? new List<User>();
            }
        }

        private async Task WriteUsersToFile(List<User> users)
        {
            using (var stream = File.Create(UserFilePath))
            {
                await JsonSerializer.SerializeAsync(stream, users);
            }
        }
    }
}
