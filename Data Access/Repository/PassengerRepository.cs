using Data_Access.Context;
using Data_Access.Mappers;
using Data_Access.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    public class PassengerRepository
    {
        public static Data_Transfer_Objects.Model.Passenger GetByID(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger passenger = context.Passengers.Find(id);
                if (passenger == null) { return null; }
                return PassengerMapper.MapFromDB(passenger);
            }
        }

        public static Data_Transfer_Objects.Model.Passenger Add(Data_Transfer_Objects.Model.Passenger passenger)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger added = PassengerMapper.MapToDB(passenger);
                context.Passengers.Add(added);
                context.SaveChanges();
                Console.WriteLine(added.Id);
                
                return PassengerMapper.MapFromDB(added);
            }
        }

        public static Data_Transfer_Objects.Model.Passenger Remove(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger passenger = context.Passengers.Find(id);
                if (passenger == null) { return null; }
                context.Passengers.Remove(passenger);
                context.SaveChanges();
                return PassengerMapper.MapFromDB( passenger);
            }
        }

        public static Data_Transfer_Objects.Model.Passenger Update(Data_Transfer_Objects.Model.Passenger updatedPassenger)
        {
            using (FerryContext context = new FerryContext())
            {
                Passenger passenger = context.Passengers.Find(updatedPassenger.Id);
                if (passenger == null) { return null; }

                Passenger updated = PassengerMapper.Update(passenger, updatedPassenger);
                context.SaveChanges();
                return PassengerMapper.MapFromDB(updated);
            }
        }
    }
}
