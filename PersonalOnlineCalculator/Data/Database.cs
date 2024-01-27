using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using PersonalOnlineCalculator.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PersonalOnlineCalculator.Data
{

    public static class Database
    {
        public static User GetUser(int id)
        {
            using var context = DataContext.Instance;

            var user = context.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public static Calculation GetCalculation(int id)
        {
            using var context = DataContext.Instance;

            var calculation = context.Calculations.FirstOrDefault(c => c.Id == id);

            return calculation;
        }

        public static List<Calculation> GetCalculationsForUser(int userId)
        {
            using var context = DataContext.Instance;

            var calculations = from uc in context.User_Calculations
                               join c in context.Calculations on uc.CalculationId equals c.Id
                               where uc.UserId == userId
                               select c;

            return calculations.ToList();
        }

        public static void AddUser(string username, string email, string password)
        {
            using var context = DataContext.Instance;

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = password
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public static void AddUser(User newUser)
        {
            using var context = DataContext.Instance;

            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public static void DeleteUser(int id)
        {
            using var context = DataContext.Instance;

            var userToDelete = context.Users.Find(id);

            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }

        public static void DeleteUser(User userToDelete)
        {
            using var context = DataContext.Instance;

            var existingUser = context.Users.Find(userToDelete.Id);

            if (existingUser != null)
            {
                context.Users.Remove(existingUser);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(User updatedUser)
        {
            using var context = DataContext.Instance;

            var existingUser = context.Users.Find(updatedUser.Id);

            if (existingUser != null)
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Email = updatedUser.Email;
                existingUser.PasswordHash = updatedUser.PasswordHash;

                context.SaveChanges();
            }
        }

        public static void UpdatePassword(int id, string password)
        {
            using var context = DataContext.Instance;

            var existingUser = context.Users.Find(id);

            if (existingUser != null)
            {
                existingUser.PasswordHash = password;
                context.SaveChanges();
            }
        }

        public static void UpdatePassword(User user, string password)
        {
            using var context = DataContext.Instance;

            var existingUser = context.Users.Find(user.Id);

            if (existingUser != null)
            {
                existingUser.PasswordHash = password;
                context.SaveChanges();
            }
        }

        public static void AddCalculation(int id, string expression, string result)
        {
            using var context = DataContext.Instance;

            var newCalculation = new Calculation
            {
                Expression = expression,
                Result = result,
            };

            context.Calculations.Add(newCalculation);
            context.SaveChanges(); // Save to get the new calculation's ID

            // Associate the calculation with the user in the many-to-many table
            var userCalculation = new User_Calculations
            {
                UserId = id,
                CalculationId = newCalculation.Id,
            };

            context.User_Calculations.Add(userCalculation);
            context.SaveChanges();
        }

        public static void AddCalculation(int id, Calculation newCalculation)
        {
            using var context = DataContext.Instance;

            context.Calculations.Add(newCalculation);
            context.SaveChanges(); // Save to get the new calculation's ID

            // Associate the calculation with the user in the many-to-many table
            var userCalculation = new User_Calculations
            {
                UserId = id,
                CalculationId = newCalculation.Id,
            };

            context.User_Calculations.Add(userCalculation);
            context.SaveChanges();
        }

        public static void DeleteCalculation(int calculationId)
        {
            using var context = DataContext.Instance;

            // Remove the calculation from the many-to-many table
            var userCalculations = context.User_Calculations.Where(uc => uc.CalculationId == calculationId).ToList();
            context.User_Calculations.RemoveRange(userCalculations);

            // Remove the calculation from the Calculations table
            var calculationToDelete = context.Calculations.Find(calculationId);
            if (calculationToDelete != null)
            {
                context.Calculations.Remove(calculationToDelete);
                context.SaveChanges();
            }
        }

        public static User GetUserByEmail(string email)
        {
            using var context = DataContext.Instance;
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public static User GetUserByUsername(string username)
        {
            using var context = DataContext.Instance;
            return context.Users.FirstOrDefault(u => u.Username == username);
        }

    }
}