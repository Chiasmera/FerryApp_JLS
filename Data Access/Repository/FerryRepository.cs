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


        //public static HashSet<Data_Transfer_Objects.Model.Car> GetCars(int id)
        //{
        //    using (FerryContext context = new FerryContext())
        //    {
        //        if (id < 1) { return null; }
        //        Ferry ferry = context.Ferries.Find(id);
        //        if (ferry == null) { return null; }
        //        return CarMapper.MapAllFromDB(ferry.Cars);
        //    }

        //}

        //public static Data_Transfer_Objects.Model.Car AddCarToFerry (int ferryID, Data_Transfer_Objects.Model.Car car, int driverID)
        //{
        //    using (FerryContext context = new FerryContext())
        //    {
        //        //Find ferry in db
        //        Ferry ferry = context.Ferries.Find(ferryID);
        //        if (ferry == null) { return null; }

        //        //convert car to model car
        //        Car mappedCar = CarMapper.MapToDB(car);

        //        //find passenger in db
        //        Passenger passenger = context.Passengers.Find(driverID);
        //        if (passenger == null) { return null; }

        //        //Set driver of car
        //        mappedCar.Driver = passenger;

        //        //add car to ferry
        //        Car result = ferry.AddCar(mappedCar);

        //        //save changes to db
        //        context.SaveChanges();

        //        if (result != null)
        //        {
        //            return car;
        //        } else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
