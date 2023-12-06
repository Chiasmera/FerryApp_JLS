using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    public class CarRepository
    {
        public static Data_Transfer_Objects.Model.Car GetByID(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == id).FirstOrDefault();
                if (car == null) { return null; }
                return CarMapper.MapFromDB(car);
            }
        }

        public static Data_Transfer_Objects.Model.Car Add(Data_Transfer_Objects.Model.Car car)
        {
            using (FerryContext context = new FerryContext())
            {
                Car dbCar = CarMapper.MapToDB(car);

                foreach (int id in car.Passengers)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    dbCar.AddPassenger(passenger);
                }

                context.Cars.Add(dbCar);
                context.SaveChanges();
                return CarMapper.MapFromDB(dbCar);
            }
        }

        public static Data_Transfer_Objects.Model.Car Remove(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == id).FirstOrDefault();
                if (car == null) { return null; }
                context.Cars.Remove(car);
                context.SaveChanges();
                return CarMapper.MapFromDB(car);
            }
        }

        public static Data_Transfer_Objects.Model.Car Update(Data_Transfer_Objects.Model.Car updatedCar)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == updatedCar.Id).FirstOrDefault();
                if (car == null) { return null; }

                //Update car itself
                Car updated = CarMapper.Update(car, updatedCar);

                //Update Passengers
                HashSet<int> ids = new HashSet<int>(updatedCar.Passengers);
                foreach(Passenger passenger in car.Passengers)
                {
                    car.RemovePassenger(passenger);
                }

                foreach (Passenger passenger in car.Passengers)
                {
                    if (ids.Contains(passenger.Id))
                    {
                        car.AddPassenger(passenger);
                        ids.Remove(passenger.Id);
                    }
                }

                foreach (int id in ids)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    car.AddPassenger(passenger);
                }


                context.SaveChanges();
                return CarMapper.MapFromDB(updated);
            }
        }


        public static HashSet<Data_Transfer_Objects.Model.Passenger> AddPassengers(int carID, HashSet<int> driverIDs)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Include(c => c.Passengers).Where(c => c.Id == carID).FirstOrDefault();
                if (car == null) { return null; }

                foreach (int id in driverIDs)
                {
                    Passenger passenger = context.Passengers.Find(id);
                    if (passenger == null) { return null; }
                    car.AddPassenger(passenger);
                }

                context.SaveChanges();
                return PassengerMapper.MapAllFromDB(car.Passengers);
            }
        }
    }
}
