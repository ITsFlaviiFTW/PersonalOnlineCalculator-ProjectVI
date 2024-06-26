﻿using Pomelo.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using PersonalOnlineCalculator.Models;
using System.Collections.Generic;

public class DataContext : DbContext
{
    //The DataContext Instance creates a replication of our models in the program that we can refer to for manipulating data on the models in the instance of our program
    //and then we can apply those modifications to our database using commands like context.SaveChanges() which will update our database with whatever we have done to the models
    //when our program was running
    public static DataContext Instance
    {
        get
        {
            return new DataContext();
        }
    }
    public DataContext()
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Calculation> Calculations { get; set; }
    public DbSet<User_Calculations> User_Calculations { get; set; }

    // Creates the connection between the program and the database so that we can save the changes from our models and enact them on the database in real time
    // This logs into our database server we have running on AWS so as long as the program has the credentials it doesn't need a local copy of the database
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

        optionsBuilder.UseMySql("Server=db-online-calculator.cfl4q3qmo0sa.us-east-2.rds.amazonaws.com;Port=3306;Database=db-online-calculator;User=admin;Password=ottersarecool;", serverVersion, options =>
        {
            options.EnableStringComparisonTranslations();
        });
    }
}