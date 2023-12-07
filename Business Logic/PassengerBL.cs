using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    /// <summary>
    /// Responsible for Bussiness Logic concerning the management of Passengers
    /// </summary>
    public class PassengerBL
    {
        /// <summary>
        /// Retrieves a specific passenger by ID
        /// </summary>
        /// <param name="id">the ID of the Passenger</param>
        /// <returns>the retrieved passenger if successfull, null otherwise</returns>
        public Passenger GetByID(int id)
        {
            if (id < 1) { return null; }
            return PassengerRepository.Get(id);
        }

        /// <summary>
        /// Adds a Passenger to the system
        /// </summary>
        /// <param name="passenger">the passenger to add</param>
        /// <returns>the new Passenger if successfull, null otherwise</returns>
        public Passenger Add(Passenger passenger)
        {
            if (passenger == null) { return null; }
            return PassengerRepository.Add(passenger);
        }

        /// <summary>
        /// Updates a passenger in the database
        /// </summary>
        /// <param name="updated">a Passenger containing the new values, and the ID of the passenger to update</param>
        /// <returns>the updated passenger if successfull, null otherwise</returns>
        public Passenger Update(Passenger updated)
        {
            if (updated == null) { return null; }
            return PassengerRepository.Update(updated);
        }

        /// <summary>
        /// Removes a passenger from the database
        /// </summary>
        /// <param name="passengerID">the ID of the passenger to remove</param>
        /// <returns>the removed passenger if successfull, null otherwise</returns>
        public Passenger Remove(int passengerID)
        {
            if (passengerID < 1) { return null; }
            return PassengerRepository.Remove(passengerID);
        }


    }
}
