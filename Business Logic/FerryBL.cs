using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class FerryBL
    {
        public HashSet<Ferry> GetAll()
        {
            return FerryRepository.GetAll();
        }

        public Ferry GetByID(int id)
        {
            if (id == null) { return null; }
            return FerryRepository.Get(id);
        }

        //public HashSet<Car> GetCars(int id)
        //{
        //    if (id == null) { return null;  }
        //    return FerryRepository.GetCars(id);
        //}

        //public Car AddCar(int ferryID, Car car, int driverID)
        //{
        //    if (ferryID == null || car == null || driverID == null) { return null; }
        //    return FerryRepository.AddCarToFerry(ferryID, car, driverID);            
        //}

        public Ferry Add (Ferry ferry)
        {
            if (ferry == null) { return null; }
            return FerryRepository.Add(ferry);
        }
    }
}
