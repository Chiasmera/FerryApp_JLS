using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Mappers
{
    /// <summary>
    /// Maps Ferries from model classes to data transfer objects, and vice versa
    /// </summary>
    internal class FerryMapper
    {
        /// <summary>
        /// Maps a Ferry model from the database to a corresponding Data Transfer Object 
        /// </summary>
        /// <param name="ferry">the Model Ferry to map</param>
        /// <returns>a Data Transfer Object representation of the provided car</returns>
        internal static Data_Transfer_Objects.Model.Ferry MapFromDB(Ferry ferry)
        {
            if (ferry == null) { return null; }
            Data_Transfer_Objects.Model.Ferry mappedFerry = new Data_Transfer_Objects.Model.Ferry();
            mappedFerry.Id = ferry.Id;
            mappedFerry.CarCapacity = ferry.CarCapacity;
            mappedFerry.PassengerCapacity = ferry.PassengerCapacity;
            mappedFerry.Name = ferry.Name;
            mappedFerry.PassengerPrice = ferry.PassengerPrice;
            mappedFerry.CarPrice = ferry.CarPrice;
            foreach (Car car in ferry.Cars)
            {
                mappedFerry.Cars.Add(CarMapper.MapFromDB(car));
            }
            foreach (Passenger passenger in ferry.Passengers)
            {
                mappedFerry.Passengers.Add(passenger.Id);
            }
            return mappedFerry;
        }

        /// <summary>
        /// Maps a Data Transfer Object to a coresponding Model Ferry
        /// </summary>
        /// <param name="ferry">The Data Transfer Object to map</param>
        /// <returns>a Model Ferry representation of the provided DTO</returns>
        internal static Ferry MapToDB(Data_Transfer_Objects.Model.Ferry ferry)
        {
            if (ferry == null) { return null; }
            Ferry mappedFerry = new Ferry();
            mappedFerry.Id = ferry.Id;
            mappedFerry.CarCapacity = ferry.CarCapacity;
            mappedFerry.PassengerCapacity = ferry.PassengerCapacity;
            mappedFerry.Name = ferry.Name;
            mappedFerry.PassengerPrice = ferry.PassengerPrice;
            mappedFerry.CarPrice = ferry.CarPrice;
            return mappedFerry;
        }

        /// <summary>
        /// Updates a Model Ferry with the mapped values from a Data Transfer Object Ferry
        /// </summary>
        /// <param name="oldFerry">the Model Ferry to update</param>
        /// <param name="updatedFerry">The Data Transfer Object Ferry with the updated values</param>
        /// <returns>A Model Ferry with updated values</returns>
        internal static Ferry Update(Ferry oldFerry, Data_Transfer_Objects.Model.Ferry updatedFerry)
        {
            if (oldFerry == null || updatedFerry == null) { return null; }
            oldFerry.Name = updatedFerry.Name;
            oldFerry.CarCapacity = updatedFerry.CarCapacity;
            oldFerry.PassengerCapacity = updatedFerry.PassengerCapacity;
            oldFerry.CarPrice =     updatedFerry.CarPrice;
            oldFerry.PassengerPrice = updatedFerry.PassengerPrice;
            return oldFerry;
        }

        /// <summary>
        /// Maps a set of Ferry models from the database to a corresponding set of Data Transfer Objects
        /// </summary>
        /// <param name="ferries">a Set of Model Ferries to map</param>
        /// <returns>a Set of Data Transfer Object Ferries</returns>
        internal static HashSet<Data_Transfer_Objects.Model.Ferry> MapAllFromDB(IQueryable<Ferry> ferries)
        {
            if (ferries.IsNullOrEmpty()) { return null; }
            HashSet<Data_Transfer_Objects.Model.Ferry> map = new HashSet<Data_Transfer_Objects.Model.Ferry>();
            foreach (Ferry ferry in ferries)
            {
                Data_Transfer_Objects.Model.Ferry mappedFerry = new Data_Transfer_Objects.Model.Ferry();
                mappedFerry.Id = ferry.Id;
                mappedFerry.CarCapacity = ferry.CarCapacity;
                mappedFerry.PassengerCapacity = ferry.PassengerCapacity;
                mappedFerry.Name = ferry.Name;
                mappedFerry.PassengerPrice = ferry.PassengerPrice;
                mappedFerry.CarPrice = ferry.CarPrice;
                map.Add(mappedFerry);
                foreach (Car car in ferry.Cars)
                {
                    mappedFerry.Cars.Add(CarMapper.MapFromDB(car));
                }
                foreach (Passenger passenger in ferry.Passengers)
                {
                    mappedFerry.Passengers.Add(passenger.Id);
                }
            }
            return map;
        }
    }
}
