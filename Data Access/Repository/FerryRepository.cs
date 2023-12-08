using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    /// <summary>
    /// Responsible for managing Ferry operations in the database
    /// </summary>
    public class FerryRepository
    {
        /// <summary>
        /// Retrieves all Ferries from the database
        /// </summary>
        /// <returns>all Ferries in the database, as a set</returns>
        public static HashSet<Data_Transfer_Objects.Model.Ferry> GetAll()
        {
            using (FerryContext context = new FerryContext())
            {
                IQueryable<Ferry> ferries = context.Ferries
                    .Include(f => f.Cars).ThenInclude(c => c.Passengers)
                    .Include(f => f.Passengers);
                return FerryMapper.MapAllFromDB(ferries);
            }
        }

        /// <summary>
        /// Retrieves a specific Ferry by its ID from the database
        /// </summary>
        /// <param name="id">Id of the ferry</param>
        /// <returns>the ferry with the given ID if successful, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Ferry Get(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                if (id < 1) { return null; }
                Ferry ferry = context.Ferries
                    .Include(f => f.Cars).ThenInclude(c => c.Passengers)
                    .Include(f => f.Passengers)
                    .Where(f => f.Id == id)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                return FerryMapper.MapFromDB(ferry);
            }
        }

        /// <summary>
        /// Adds a ferry to the databse
        /// </summary>
        /// <param name="ferry">The Ferry to add to the database</param>
        /// <returns>The added ferry if successfull, null otherwies</returns>
        public static Data_Transfer_Objects.Model.Ferry Add(Data_Transfer_Objects.Model.Ferry ferry)
        {
            using (FerryContext context = new FerryContext())
            {
                if (ferry == null) { return null; }
                Ferry dbFerry = FerryMapper.MapToDB(ferry);
                context.Ferries.Add(dbFerry);
                context.SaveChanges();

                return FerryMapper.MapFromDB( dbFerry);
            }
        }

        /// <summary>
        /// Updates a ferry in the database to match the given ferry
        /// </summary>
        /// <param name="ferry">A Ferry with the updated values and the id of the ferry to update</param>
        /// <returns>the updated ferry from the database if successfull, or null otherwise</returns>
        public static Data_Transfer_Objects.Model.Ferry Update(Data_Transfer_Objects.Model.Ferry ferry)
        {
            using (FerryContext context = new FerryContext())
            {
                if (ferry == null) { return null; }
                Ferry dbFerry = context.Ferries
                    .Include(f => f.Cars)
                    .Include(f => f.Passengers)
                    .Where(f => f.Id == ferry.Id)
                    .FirstOrDefault();
                if (dbFerry == null) { return null; }
                Ferry updated = FerryMapper.Update(dbFerry, ferry);

                context.SaveChanges();
                return FerryMapper.MapFromDB(updated);
            }
        }

        /// <summary>
        /// Removes a ferry from the database 
        /// </summary>
        /// <param name="ferry">The Ferry to remove from the database</param>
        /// <returns>the removed ferry if successful, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Ferry Remove(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                if (id < 1) { return null; }
                Ferry dbFerry = context.Ferries
                     .Include(f => f.Cars)
                    .Include(f => f.Passengers)
                    .Where(f => f.Id == id)
                    .FirstOrDefault();
                if (dbFerry == null) { return null; }

                foreach (Passenger p in dbFerry.Passengers)
                {
                    dbFerry.Passengers.Remove(p);
                }

                foreach ( Car c in dbFerry.Cars)
                {
                    dbFerry.Cars.Remove(c);
                }

                context.Ferries.Remove(dbFerry);

                context.SaveChanges();
                return FerryMapper.MapFromDB(dbFerry);
            }
        }

        /// <summary>
        /// Searches the ferry with the given id for a passenger with the given id. 
        /// Returns if the passenger is present in a car, returns that car, otherwise if the passenger is present returns the passenger. 
        /// Returns null if no passenger is found.
        /// </summary>
        /// <param name="ferryID">Id of the ferry to search</param>
        /// <param name="passengerID">Id of the passenger to find</param>
        /// <returns>A Car or Passenger if the passenger is present on the ferry, depending on whether the passenger is walking or driving. 
        /// Null if no passenger with the id exists on the ferry.</returns>
        public static Data_Transfer_Objects.Model.IFerryable HasPassenger (int ferryID, int passengerID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null)
                {
                    return null;
                }

                Passenger passenger = ferry.Passengers.Where(p => p.Id == passengerID).FirstOrDefault();
                if (passenger != null)
                {
                    return PassengerMapper.MapFromDB( passenger) as Data_Transfer_Objects.Model.IFerryable;
                }

                foreach (Car car in ferry.Cars)
                {
                    Passenger found = car.Passengers.Where(p => p.Id == passengerID).FirstOrDefault();
                    if (found != null)
                    {
                        return CarMapper.MapFromDB(car);
                    }
                }
                
                return null;
            }
        }

        /// <summary>
        /// Adds a passenger to the ferry
        /// </summary>
        /// <param name="ferryID">ID of the ferry</param>
        /// <param name="passengerID">ID of the passenger to add</param>
        /// <returns>the Passenger added to the ferry if successfull, or null otherwise</returns>
        public static Data_Transfer_Objects.Model.Passenger AddPassenger(int ferryID, int passengerID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }

                Passenger passenger = context.Passengers.Find(passengerID);
                if (passenger == null) { return null; }
                Passenger added = ferry.AddPassenger(passenger);
                if (added != null)
                {
                    context.SaveChanges();
                }
                return PassengerMapper.MapFromDB(added);
            }
        }

        /// <summary>
        /// Removes a passenger from a ferry in the database
        /// </summary>
        /// <param name="ferryID">ID of the ferry</param>
        /// <param name="passengerID">ID of the passenger to remove</param>
        /// <returns>the Removed passenger if successfull, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Passenger RemovePassenger(int ferryID, int passengerID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries
                      .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                Passenger passenger = context.Passengers.Find(passengerID);
                if (passenger == null) { return null; }
                Passenger removed = ferry.RemovePassenger(passenger);
                if (removed != null)
                {
                    context.SaveChanges();
                }
                return PassengerMapper.MapFromDB(removed);
            }
        }

        /// <summary>
        /// Adds a car to the ferry
        /// </summary>
        /// <param name="ferryID">ID of the ferry</param>
        /// <param name="carID">Id of the car to add</param>
        /// <returns>The added car if successfull, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Car AddCar(int ferryID, int carID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                Car car = context.Cars.Include("Passengers").Where(c => c.Id == carID).FirstOrDefault(); ;
                if (car == null) { return null; }
                Car added = ferry.AddCar(car);
                if (added != null)
                {
                    context.SaveChanges();
                }
                return CarMapper.MapFromDB(added);
            }
        }

        /// <summary>
        /// Removes a car from a ferry in the database
        /// </summary>
        /// <param name="ferryID">the ID of the ferry</param>
        /// <param name="carID">the Id of the car to remove</param>
        /// <returns>The removed car if successful, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Car RemoveCar(int ferryID, int carID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                Car car = context.Cars.Include("Passengers").Where(c => c.Id == carID).FirstOrDefault(); ;
                if (car == null) { return null; }
                Car removed = ferry.RemoveCar(car);
                if (removed != null)
                {
                    context.SaveChanges();
                }
                return CarMapper.MapFromDB(removed);
            }
        }

        /// <summary>
        /// TESTDATA ONLY. Clears the whole database. Should be removed in actual program.
        /// </summary>
        public static void TESTClearAllData ()
        {
            using (FerryContext context = new FerryContext())
            {
                context.Ferries.RemoveRange(context.Ferries);
                context.Cars.RemoveRange(context.Cars);
                context.Passengers.RemoveRange(context.Passengers);
                context.SaveChanges();
            }
        }
    }
}
