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
    internal class FerryContext : DbContext
    {
        //Ensures database is created
        public FerryContext()
        {
            bool created = Database.EnsureCreated();
            if (created)
            {
                Debug.WriteLine("Database created");
            }

        }

        //Configuration info for database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TERRA\\SQLEXPRESS;Initial Catalog=FerryDB;Integrated Security=SSPI; TrustServerCertificate=true");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        //DBsets for use in database
        public DbSet<Ferry> Ferries { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}
