using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
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
                IQueryable<Ferry> ferries = context.Ferries
                    .Include(f => f.Cars).ThenInclude(c => c.Passengers)
                    .Include(f => f.Passengers);
                return FerryMapper.MapAllFromDB(ferries);
            }
        }

        public static Data_Transfer_Objects.Model.Ferry Get(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                if (id < 1) { return null; }
                Ferry ferry = context.Ferries
                    .Include(f => f.Cars).ThenInclude(c => c.Passengers)
                    .Include(f => f.Passengers)
                    .Where(f => f.Id == id)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                return FerryMapper.MapFromDB(ferry);
            }
        }

        //public static double GetIncome(int id)
        //{
        //    using (FerryContext context = new FerryContext())
        //    {
        //        if (id < 1) { return -1; }
        //        Ferry ferry = context.Ferries
        //            .Include(f => f.Cars).ThenInclude(c => c.Passengers)
        //            .Include(f => f.Passengers)
        //            .Where(f => f.Id == id)
        //            .FirstOrDefault();
        //        if (ferry == null) { return -1; }
        //        return ferry.GetTotalPrice();
        //    }
        //}

        public static Data_Transfer_Objects.Model.Ferry Add(Data_Transfer_Objects.Model.Ferry ferry)
        {
            using (FerryContext context = new FerryContext())
            {
                if (ferry == null) { return null; }
                context.Ferries.Add(FerryMapper.MapToDB(ferry));
                context.SaveChanges();
                return ferry;
            }
        }

        public static Data_Transfer_Objects.Model.Ferry Update(Data_Transfer_Objects.Model.Ferry ferry)
        {
            using (FerryContext context = new FerryContext())
            {
                if (ferry == null) { return null; }
                Ferry dbFerry = context.Ferries
                    .Include(f => f.Cars)
                    .Include(f => f.Passengers)
                    .Where(f => f.Id == ferry.Id)
                    .FirstOrDefault();
                if (dbFerry == null) { return null; }
                Ferry updated = FerryMapper.Update(dbFerry, ferry);

                context.SaveChanges();
                return ferry;
            }
        }

        public static Data_Transfer_Objects.Model.Passenger AddPassenger(int ferryID, int passengerID)
        {
            using (FerryContext context = new FerryContext())
            {
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }

                Passenger passenger = context.Passengers.Find(passengerID);
                if (passenger == null) { return null; }
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
                Ferry ferry = context.Ferries
                      .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
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
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                Car car = context.Cars.Include("Passengers").Where(c => c.Id == carID).FirstOrDefault(); ;
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
                Ferry ferry = context.Ferries
                    .Include("Cars")
                    .Include("Cars.Passengers")
                    .Include("Passengers")
                    .Where(f => f.Id == ferryID)
                    .FirstOrDefault();
                if (ferry == null) { return null; }
                Car car = context.Cars.Include("Passengers").Where(c => c.Id == carID).FirstOrDefault(); ;
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
