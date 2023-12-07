using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Context
{
    /// <summary>
    /// The database context for the system, managing ferries, cars and passengers
    /// </summary>
    internal class FerryContext : DbContext
    {
        public FerryContext()
        {
            bool created = Database.EnsureCreated();
            if (created)
            {
                Debug.WriteLine("Database created");
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TERRA\\SQLEXPRESS;Initial Catalog=FerryDB;Integrated Security=SSPI; TrustServerCertificate=true");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        public DbSet<Ferry> Ferries { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}
