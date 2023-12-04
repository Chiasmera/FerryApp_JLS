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
    public class CarRepository
    {
        public static Data_Transfer_Objects.Model.Car GetByID(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Find(id);
                if (car == null) { return null; }
                return CarMapper.MapFromDB(car);
            }
        }

        public static Data_Transfer_Objects.Model.Car Add(Data_Transfer_Objects.Model.Car car)
        {
            using (FerryContext context = new FerryContext())
            {
                context.Cars.Add(CarMapper.MapToDB(car));
                context.SaveChanges();
                return car;
            }
        }

        public static Data_Transfer_Objects.Model.Car Remove(int id)
        {
            using (FerryContext context = new FerryContext())
            {
                Car car = context.Cars.Find(id);
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
                Car passenger = context.Cars.Find(updatedCar.Id);
                if (passenger == null) { return null; }

                Car updated = CarMapper.Update(passenger, updatedCar);
                context.SaveChanges();
                return CarMapper.MapFromDB(updated);
            }
        }
    }
}
