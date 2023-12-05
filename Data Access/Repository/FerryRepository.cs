using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    public class FerryRepository
    {
        public static HashSet<Data_Transfer_Objects.Model.Ferry> GetAll()
        {
            using (FerryContext context = new FerryContext())
            {
                return FerryMapper.MapAllFromDB(context.Ferries);
            }
        }

        public static Data_Transfer_Objects.Model.Ferry Get(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                if (id < 1) { return null; }
                return FerryMapper.MapFromDB(context.Ferries.Find(id));
            }
        }

        public static Data_Transfer_Objects.Model.Ferry Add(Data_Transfer_Objects.Model.Ferry ferry)
        {
            using (FerryContext context = new FerryContext())
            {
                if(ferry == null) { return null; }
                context.Ferries.Add(FerryMapper.MapToDB(ferry));
                context.SaveChanges();
                return ferry;
            }
        }

        public static Data_Transfer_Objects.Model.Passenger AddPassenger(int ferryID, int passengerID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries.Find(ferryID);
                if (ferry == null) { return null; }
                Passenger passenger = context.Passengers.Find(passengerID);
                if(passenger == null) { return null; }
                Passenger added = ferry.AddPassenger(passenger);
                if (added != null)
                {
                    context.SaveChanges();
                }
                return PassengerMapper.MapFromDB(added);
            }
        }

        public static Data_Transfer_Objects.Model.Passenger RemovePassenger(int ferryID, int passengerID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries.Find(ferryID);
                if (ferry == null) { return null; }
                Passenger passenger = context.Passengers.Find(passengerID);
                if (passenger == null) { return null; }
                Passenger removed = ferry.RemovePassenger(passenger);
                if (removed != null)
                {
                    context.SaveChanges();
                }
                return PassengerMapper.MapFromDB(removed);
            }
        }

        public static Data_Transfer_Objects.Model.Car AddCar(int ferryID, int carID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries.Find(ferryID);
                if (ferry == null) { return null; }
                Car car = context.Cars.Find(carID);
                if (car == null) { return null; }
                Car added = ferry.AddCar(car);
                if (added != null)
                {
                    context.SaveChanges();
                }
                return CarMapper.MapFromDB(added);
            }
        }

        public static Data_Transfer_Objects.Model.Car RemoveCar(int ferryID, int carID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries.Find(ferryID);
                if (ferry == null) { return null; }
                Car car = context.Cars.Find(carID);
                if (car == null) { return null; }
                Car removed = ferry.RemoveCar(car);
                if (removed != null)
                {
                    context.SaveChanges();
                }
                return CarMapper.MapFromDB(removed);
            }
        }



    }
}
