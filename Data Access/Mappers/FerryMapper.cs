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
    internal class FerryMapper
    {
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
                mappedFerry.Cars.Add(car.Id);
            }
            foreach (Passenger passenger in ferry.Passengers)
            {
                mappedFerry.Passengers.Add(passenger.Id);
            }
            return mappedFerry;
        }

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
                    mappedFerry.Cars.Add(car.Id);
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
