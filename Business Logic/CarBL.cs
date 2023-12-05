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
    public class CarBL
    {
        public Car GetByID(int id)
        {
            if (id < 1) { return null; }
            return CarRepository.GetByID(id);
        }


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
