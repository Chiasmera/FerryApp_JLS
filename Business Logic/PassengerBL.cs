using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class PassengerBL
    {
        public Passenger GetByID(int id)
        {
            if (id < 1) { return null; }
            return PassengerRepository.GetByID(id);
        }


        public Passenger Add(Passenger passenger)
        {
            if (passenger == null) { return null; }
            return PassengerRepository.Add(passenger);
        }

        public Passenger Update(Passenger updated)
        {
            if (updated == null) { return null; }
            return PassengerRepository.Update(updated);
        }

        public Passenger Remove(int passengerID)
        {
            if (passengerID < 1) { return null; }
            return PassengerRepository.Remove(passengerID);
        }


    }
}
