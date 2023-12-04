using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
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
            Data_Transfer_Objects.Model.Car mappedCar = new Data_Transfer_Objects.Model.Car();
            mappedCar.Id = car.Id;
            mappedCar.Registration = car.Registration;
            mappedCar.Weight = car.Weight;
            return mappedCar;
        }

        internal static HashSet< Data_Transfer_Objects.Model.Car> MapAllFromDB(HashSet<Car> cars)
        {
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
           Car mappedCar = new Car();
            mappedCar.Id = car.Id;
            mappedCar.Registration = car.Registration;
            mappedCar.Weight = car.Weight;
            return mappedCar;
        }

        internal static Car Update(Car oldCar, Data_Transfer_Objects.Model.Car updatedCar)
        {
            oldCar.Name = updatedCar.Name;
            oldCar.Gender = updatedCar.Gender;
            return oldCar;
        }
    }
}
