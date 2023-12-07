using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Mappers
{
    /// <summary>
    /// Maps Passengers from model classes to data transfer objects, and vice versa
    /// </summary>
    internal class PassengerMapper
    {
        /// <summary>
        /// Maps a Passenger model from the database to a corresponding Data Transfer Object
        /// </summary>
        /// <param name="passenger">the Model Passenger to map</param>
        /// <returns>a Data Transfer Object representation of the provided passenger</returns>
        internal static Data_Transfer_Objects.Model.Passenger MapFromDB(Passenger passenger)
        {
            if (passenger == null) { return null; }
            Data_Transfer_Objects.Model.Passenger mappedPassenger = new Data_Transfer_Objects.Model.Passenger();
            mappedPassenger.Id = passenger.Id;
            mappedPassenger.Name = passenger.Name;
            mappedPassenger.Gender = passenger.Gender;
            return mappedPassenger;
        }

        /// <summary>
        /// Maps set of a Passenger models from the database to a corresponding set of  Data Transfer Objects
        /// </summary>
        /// <param name="passengers">a set of Model Passengers to map</param>
        /// <returns>a set of Data Transfer Object representations of the provided passengers</returns>
        internal static HashSet<Data_Transfer_Objects.Model.Passenger> MapAllFromDB(HashSet<Passenger> passengers)
        {
            if (passengers.IsNullOrEmpty()) { return null; }
            HashSet<Data_Transfer_Objects.Model.Passenger> map = new HashSet<Data_Transfer_Objects.Model.Passenger>();
            foreach (Passenger passenger in passengers)
            {
                Data_Transfer_Objects.Model.Passenger mappedPassenger = new Data_Transfer_Objects.Model.Passenger();
                mappedPassenger.Id = passenger.Id;
                mappedPassenger.Name= passenger.Name;
                mappedPassenger.Gender= passenger.Gender;
                map.Add(mappedPassenger);
            }
            return map;
        }

        /// <summary>
        /// Maps a Data Transfer Object Passenger to a corresponding Model Passenger
        /// </summary>
        /// <param name="passenger">The Data Transfer Object Passenger to map</param>
        /// <returns>a Model Passenger representation of the provided DTO</returns>
        internal static Passenger MapToDB(Data_Transfer_Objects.Model.Passenger passenger)
        {
            if (passenger == null) { return null; }
            Passenger mappedPassenger = new Passenger();
            mappedPassenger.Id = passenger.Id;
            mappedPassenger.Name = passenger.Name;
            mappedPassenger.Gender = passenger.Gender;
            return mappedPassenger;
        }

        /// <summary>
        /// Updates a Model Passenger to match the mapped values of a Data Transfer Object Passenger
        /// </summary>
        /// <param name="oldPassenger">the Model Passenger to update</param>
        /// <param name="updatedPassenger">the Data Transfer Object Passenger with the updated values</param>
        /// <returns>a Model Passenger with updated values</returns>
        internal static Passenger Update (Passenger oldPassenger, Data_Transfer_Objects.Model.Passenger updatedPassenger)
        {
            if (updatedPassenger == null) { return null; }
            oldPassenger.Name = updatedPassenger.Name;
            oldPassenger.Gender = updatedPassenger.Gender;
            return oldPassenger;
        }
    }
}
