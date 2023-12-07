using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    /// <summary>
    /// Responsible for Business Logic regarding the management of cars in the system
    /// </summary>
    public class CarBL
    {
        /// <summary>
        /// finds and returns a Car with the given ID
        /// </summary>
        /// <param name="id">the ID of the car to find</param>
        /// <returns>a Car with the given id if successfull, null otherwise</returns>
        public Car GetByID(int id)
        {
            if (id < 1) { return null; }
            return CarRepository.GetByID(id);
        }


        /// <summary>
        /// Adds a Car to the Database, returning the new Car if successfull
        /// </summary>
        /// <param name="car">The Car to add to the database</param>
        /// <returns>the newly added car if successfull, null otherwise</returns>
        public Car Add(Car car)
        {
            if (car == null
                || car.DriverID == null
                || car.Passengers.Count > 5
                || car.Passengers.Count < 1
                || !car.Passengers.Contains(car.DriverID))
            {
                return null;
            }
            return CarRepository.Add(car);
        }

        /// <summary>
        /// Updates an existing car in the database, to match the Car provided
        /// </summary>
        /// <param name="car">The Car with the new values, and ID matching the car to update</param>
        /// <returns>Returns the updated Car id successfull, null otherwise</returns>
        public Car Update(Car car)
        {
            if (car == null
                || car.DriverID == null
                || car.Passengers.Count > 5
                || car.Passengers.Count < 1
                || !car.Passengers.Contains(car.DriverID))
            {
                return null;
            }
            return CarRepository.Update(car);
        }

        /// <summary>
        /// Removes a Car with the given ID from the database
        /// </summary>
        /// <param name="carID">The id of the car to remove from the database</param>
        /// <returns>the removed Car if successfull, null otherwise</returns>
        public Car Remove(int carID)
        {
            if (carID < 1) { return null; }
            return CarRepository.Remove(carID);
        }

        public HashSet<Passenger> AddPassengers(int carID, HashSet<int> passengerIDs)
        {
            if (passengerIDs.IsNullOrEmpty()) { return null; }
            return CarRepository.AddPassengers(carID, passengerIDs);
        }
    }
}
