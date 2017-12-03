using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace RageOS
{
    public class MyConfiguration
    {
        public static string GetConnectionString()
        {
            MySqlConnectionStringBuilder CS = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "rageos",
                UserID = "rageos",
                Password = "rageos"
            };

            return CS.ToString();
        }
    }

    public class DBContext : DbContext
    {
        public DbSet<Database.Models.Account> Account { get; set; }
        public DbSet<Database.Models.Character> Character { get; set; }
        public DbSet<Database.Models.Communication> Communication { get; set; }
        public DbSet<Database.Models.Company> Company { get; set; }
        public DbSet<Database.Models.HouseKey> HouseKey { get; set; }
        public DbSet<Database.Models.House> House { get; set; }
        public DbSet<Database.Models.Inventory> Inventory { get; set; }
        public DbSet<Database.Models.Job> Job { get; set; }
        public DbSet<Database.Models.Message> Message { get; set; }
        public DbSet<Database.Models.Skill> Skill { get; set; }
        public DbSet<Database.Models.vehicleinfo> Vehicleinfo { get; set; }
        public DbSet<Database.Models.Vehicle> Vehicle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(MyConfiguration.GetConnectionString());
        }
    }
}
