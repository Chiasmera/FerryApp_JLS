using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    /// <summary>
    /// Responsible for Business Logic regarding the management of ferries in the system
    /// </summary>
    public class FerryBL
    {
        /// <summary>
        /// Retrieves all Ferries in the database
        /// </summary>
        /// <returns>A collection of all Ferries</returns>
        public HashSet<Ferry> GetAll()
        {
            return FerryRepository.GetAll();
        }

        /// <summary>
        /// Retrieves a specific ferry by its ID
        /// </summary>
        /// <param name="id">The id for the Ferry</param>
        /// <returns>the ferry with the provided ID if succcessfull, null otherwise</returns>
        public Ferry GetByID(int id)
        {
            if (id == null) { return null; }
            return FerryRepository.Get(id);
        }

        /// <summary>
        /// Adds a ferry to the database
        /// </summary>
        /// <param name="ferry">The Ferry to add</param>
        /// <returns>the added Ferry if successfull, null otherwise</returns>
        public Ferry Add (Ferry ferry)
        {
            if (ferry == null) { return null; }
            return FerryRepository.Add(ferry);
        }

        /// <summary>
        /// Updates a ferry in the database to match the provided Ferry
        /// </summary>
        /// <param name="ferry">A Ferry with the new values, and ID to match the ferry to update</param>
        /// <returns>the updated ferry if successfull, null otherwise</returns>
        public Ferry Update(Ferry ferry)
        {
            if (ferry == null) { return null; }
            return FerryRepository.Update(ferry);
        }

        public Ferry Remove(int id)
        {
            if (id < 1) { return null; }
            return FerryRepository.Remove(id);
        }

        /// <summary>
        /// Adds a passenger to a ferry
        /// </summary>
        /// <param name="ferryID">The ID of the ferry the passenger should be addded to</param>
        /// <param name="passengerID">the ID of the passenger to add</param>
        /// <returns>the added passenger if successfull, null othwerwise</returns>
        public Passenger AddPassenger(int ferryID, int passengerID)
        {
            //TODO - check if passenger is already in a car somewhere on the ferry, if so remove them (unless they are driver)
            if (ferryID < 0 || passengerID < 0) {  return null; }
            return FerryRepository.AddPassenger(ferryID, passengerID);
        }



        /// <summary>
        /// Removes a specific passenger from a ferry
        /// </summary>
        /// <param name="ferryID">The ID of the ferry to remove the passenger from</param>
        /// <param name="passengerID">The ID of the passenger to remove</param>
        /// <returns>the id of the removed passenger, null otherwise</returns>
        public Passenger RemovePassenger(int ferryID, int passengerID)
        {
            if (ferryID < 0 || passengerID < 0) { return null; }
            return FerryRepository.RemovePassenger(ferryID, passengerID);
        }

        /// <summary>
        /// Adds a car, including its passengers, to a ferry
        /// </summary>
        /// <param name="ferryID">The ID of the ferry to which the car should be added</param>
        /// <param name="carID">The ID of the car to add</param>
        /// <returns>the added Car if successfull, null otherwise</returns>
        public Car AddCar(int ferryID, int carID)
        {
            if (ferryID < 0 || carID < 0) { return null; }
            return FerryRepository.AddCar(ferryID, carID);
        }

        /// <summary>
        /// Removes a specific car and its passengers from the ferry
        /// </summary>
        /// <param name="ferryID">the ID of the ferry where the car should be removed</param>
        /// <param name="carID">the Id of the car to remove</param>
        /// <returns>the removed Car if successfull, null otherwise</returns>
        public Car RemoveCar(int ferryID, int carID)
        {
            if (ferryID < 0 || carID < 0) { return null; }
            return FerryRepository.RemoveCar(ferryID, carID);
        }

    }
}
