using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    /// <summary>
    /// Responsible for managing Passenger operations in the database
    /// </summary>
    public class PassengerRepository
    {
        /// <summary>
        /// Retrieves a passenger by ID from the database
        /// </summary>
        /// <param name="id">ID of the passenger</param>
        /// <returns>the passenger with the provided ID from the database</returns>
        public static Data_Transfer_Objects.Model.Passenger Get(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger passenger = context.Passengers.Find(id);
                if (passenger == null) { return null; }
                return PassengerMapper.MapFromDB(passenger);
            }
        }

        /// <summary>
        /// Adds a passenger to the database
        /// </summary>
        /// <param name="passenger">the passenger to add</param>
        /// <returns>the added passenger if successfull, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Passenger Add(Data_Transfer_Objects.Model.Passenger passenger)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger added = PassengerMapper.MapToDB(passenger);
                context.Passengers.Add(added);
                context.SaveChanges();
                Console.WriteLine(added.Id);
                
                return PassengerMapper.MapFromDB(added);
            }
        }

        /// <summary>
        /// Removes a passenger from the database
        /// </summary>
        /// <param name="id">the ID of the passenger to remove</param>
        /// <returns>the Passenger removed if successfull, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Passenger Remove(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger passenger = context.Passengers.Find(id);
                if (passenger == null) { return null; }
                context.Passengers.Remove(passenger);
                context.SaveChanges();
                return PassengerMapper.MapFromDB( passenger);
            }
        }

        /// <summary>
        /// Updates a passenger to match the values of the provided passenger
        /// </summary>
        /// <param name="updatedPassenger">a Passenger with the updated values and the ID of the passenger to update</param>
        /// <returns>The updated passenger from the database if successful, or null otherwise</returns>
        public static Data_Transfer_Objects.Model.Passenger Update(Data_Transfer_Objects.Model.Passenger updatedPassenger)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger passenger = context.Passengers.Find(updatedPassenger.Id);
                if (passenger == null) { return null; }

                Passenger updated = PassengerMapper.Update(passenger, updatedPassenger);
                context.SaveChanges();
                return PassengerMapper.MapFromDB(updated);
            }
        }
    }
}
