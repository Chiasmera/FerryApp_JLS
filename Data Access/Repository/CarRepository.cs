using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    /// <summary>
    /// Responsible for operations on cars in the database, via Entity Framework
    /// </summary>
    public class CarRepository
    {
        /// <summary>
        /// Retrieves a specific car from the database
        /// </summary>
        /// <param name="id">The Id of the Car</param>
        /// <returns>The car with the given id, or null if none is found</returns>
        public static Data_Transfer_Objects.Model.Car GetByID(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == id).FirstOrDefault();
                if (car == null) { return null; }
                return CarMapper.MapFromDB(car);
            }
        }

        /// <summary>
        /// Adds a car to the database
        /// </summary>
        /// <param name="car">the Car to add</param>
        /// <returns>Thenewly added car if successfull, or null otherwise</returns>
        public static Data_Transfer_Objects.Model.Car Add(Data_Transfer_Objects.Model.Car car)
        {
            using (FerryContext context = new FerryContext())
            {
                Car dbCar = CarMapper.MapToDB(car);

                foreach (int id in car.Passengers)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    dbCar.AddPassenger(passenger);
                }

                context.Cars.Add(dbCar);
                context.SaveChanges();
                return CarMapper.MapFromDB(dbCar);
            }
        }

        /// <summary>
        /// Removes a specific car from the databse
        /// </summary>
        /// <param name="id">Id of the car to remove</param>
        /// <returns>the removec Car if successfull, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Car Remove(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == id).FirstOrDefault();
                if (car == null) { return null; }
                context.Cars.Remove(car);
                context.SaveChanges();
                return CarMapper.MapFromDB(car);
            }
        }

        /// <summary>
        /// Updates a car in the datase to match the values of the provided car
        /// </summary>
        /// <param name="updatedCar">a Car with the updated values, and the ID of the Car to update</param>
        /// <returns>The updated car from the database if sucessful, null otherwise</returns>
        public static Data_Transfer_Objects.Model.Car Update(Data_Transfer_Objects.Model.Car updatedCar)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == updatedCar.Id).FirstOrDefault();
                if (car == null) { return null; }

                //Update car itself
                Car updated = CarMapper.Update(car, updatedCar);

                //Update Passengers
                HashSet<int> ids = new HashSet<int>(updatedCar.Passengers);
                foreach(Passenger passenger in car.Passengers)
                {
                    car.RemovePassenger(passenger);
                }

                foreach (Passenger passenger in car.Passengers)
                {
                    if (ids.Contains(passenger.Id))
                    {
                        car.AddPassenger(passenger);
                        ids.Remove(passenger.Id);
                    }
                }

                foreach (int id in ids)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    car.AddPassenger(passenger);
                }


                context.SaveChanges();
                return CarMapper.MapFromDB(updated);
            }
        }

        /// <summary>
        /// Adds passengers to a car in the database
        /// </summary>
        /// <param name="carID">ID of the car</param>
        /// <param name="passengerIDs">A set of IDs for the passengers to add</param>
        /// <returns>A set of the Passengers added to the car</returns>
        public static HashSet<Data_Transfer_Objects.Model.Passenger> AddPassengers(int carID, HashSet<int> passengerIDs)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == carID).FirstOrDefault();
                if (car == null) { return null; }

                foreach (int id in passengerIDs)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    car.AddPassenger(passenger);
                }

                context.SaveChanges();
                return PassengerMapper.MapAllFromDB(car.Passengers);
            }
        }

        /// <summary>
        /// Removes passengers from a car in the database. Will not remove the driver of a car.
        /// </summary>
        /// <param name="carID">ID of the car</param>
        /// <param name="passengerIDs">A set of IDs for the passengers to remove</param>
        /// <returns>A set of the Passengers removed from the car</returns>
        public static HashSet<Data_Transfer_Objects.Model.Passenger> RemovePassengers(int carID, HashSet<int> passengerIDs)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == carID).FirstOrDefault();
                if (car == null) { return null; }

                foreach (int id in passengerIDs)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    car.RemovePassenger(passenger);
                }

                context.SaveChanges();
                return PassengerMapper.MapAllFromDB(car.Passengers);
            }
        }
    }
}
