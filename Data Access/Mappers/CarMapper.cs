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
    /// Maps Cars from model classes to data transfer objects, and vice versa
    /// </summary>
    internal class CarMapper
    {
        /// <summary>
        /// Maps a Car model from the database to a corresponding Data Transfer Object
        /// </summary>
        /// <param name="car">the Model Car to map</param>
        /// <returns>a Data Transfer Object representation of the provided car</returns>
        internal static Data_Transfer_Objects.Model.Car MapFromDB(Car car)
        {
            if (car == null) { return null; }
            Data_Transfer_Objects.Model.Car mappedCar = new Data_Transfer_Objects.Model.Car();
            mappedCar.Id = car.Id;
            mappedCar.Registration = car.Registration;
            mappedCar.Weight = car.Weight;
            foreach (Passenger passenger in  car.Passengers)
            {
                mappedCar.Passengers.Add(passenger.Id);
            }
            mappedCar.DriverID = car.DriverID;
            return mappedCar;
        }

        /// <summary>
        /// Maps a set of Car models from the database to a corresponding set of Data Transfer Object
        /// </summary>
        /// <param name="cars">a set of Model Cars to map</param>
        /// <returns></returns>
        internal static HashSet< Data_Transfer_Objects.Model.Car> MapAllFromDB(HashSet<Car> cars)
        {
            if (cars.IsNullOrEmpty()) { return null; }
            HashSet<Data_Transfer_Objects.Model.Car> mappedCars = new HashSet<Data_Transfer_Objects.Model.Car>();
            foreach (Car car in cars) {
                Data_Transfer_Objects.Model.Car mappedCar = new Data_Transfer_Objects.Model.Car();
                mappedCar.Id = car.Id;
                mappedCar.Registration = car.Registration;
                mappedCar.Weight = car.Weight;
                mappedCars.Add(mappedCar);
            }
            return mappedCars;
        }

        /// <summary>
        /// Maps a Data Transfer Object to a corresponding Model Car
        /// </summary>
        /// <param name="car">the Data Transfer Object to map</param>
        /// <returns>A Model Car representation of the DTO Car</returns>
        internal static Car MapToDB(Data_Transfer_Objects.Model.Car car)
        {
            if (car == null) { return null; }
            Car mappedCar = new Car();
            mappedCar.Id = car.Id;
            mappedCar.Registration = car.Registration;
            mappedCar.Weight = car.Weight;
            mappedCar.DriverID = car.DriverID;
            return mappedCar;
        }

        /// <summary>
        /// Updates the values of a Model Car with the corresponding mapped values of a Data Transfer Object
        /// </summary>
        /// <param name="oldCar">the Model Car to be updated</param>
        /// <param name="updatedCar">the Data Transfer Car with updated values</param>
        /// <returns>a Model Car, updated with values from the DTO</returns>
        internal static Car Update(Car oldCar, Data_Transfer_Objects.Model.Car updatedCar)
        {
            oldCar.Registration = updatedCar.Registration;
            oldCar.Weight = updatedCar.Weight;
            oldCar.DriverID= updatedCar.DriverID;
            return oldCar;
        }
    }
}
