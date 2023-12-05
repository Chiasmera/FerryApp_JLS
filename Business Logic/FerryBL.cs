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

        public double GetIncome(int id)
        {
            if (id == null) { return -1; }
            return FerryRepository.GetIncome(id);
        }

        public Ferry Add (Ferry ferry)
        {
            if (ferry == null) { return null; }
            return FerryRepository.Add(ferry);
        }

        public Ferry Update(Ferry ferry)
        {
            if (ferry == null) { return null; }
            return FerryRepository.Update(ferry);
        }

        public Passenger AddPassenger(int ferryID, int passengerID)
        {
            if (ferryID < 0 || passengerID < 0) {  return null; }
            return FerryRepository.AddPassenger(ferryID, passengerID);
        }

        public Passenger RemovePassenger(int ferryID, int passengerID)
        {
            if (ferryID < 0 || passengerID < 0) { return null; }
            return FerryRepository.RemovePassenger(ferryID, passengerID);
        }

        public Car AddCar(int ferryID, int carID)
        {
            if (ferryID < 0 || carID < 0) { return null; }
            return FerryRepository.AddCar(ferryID, carID);
        }

        public Car RemoveCar(int ferryID, int carID)
        {
            if (ferryID < 0 || carID < 0) { return null; }
            return FerryRepository.RemoveCar(ferryID, carID);
        }

    }
}
