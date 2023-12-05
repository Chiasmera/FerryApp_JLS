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
    internal class CarMapper
    {
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

        internal static Car Update(Car oldCar, Data_Transfer_Objects.Model.Car updatedCar)
        {
            oldCar.Registration = updatedCar.Registration;
            oldCar.Weight = updatedCar.Weight;
            oldCar.DriverID= updatedCar.DriverID;
            return oldCar;
        }
    }
}
