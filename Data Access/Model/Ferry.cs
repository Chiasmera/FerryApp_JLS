using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Model
{
    internal class Ferry
    {
        //Fields -------------------------------------------------------------
        private int _id;
        private string _name;
        private int _carCapacity;
        private int _passengerCapacity;

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public int CarCapacity { get { return _carCapacity; } set { _carCapacity = value; } }
        public int PassengerCapacity { get { return _passengerCapacity; } set { _passengerCapacity = value; } }

        //Linkattributes--------------------------
        private HashSet<Car> _cars = new HashSet<Car>();
        public HashSet<Car> Cars { get { return _cars; } }

        private HashSet<Passenger> _passengers = new HashSet<Passenger>();
        public HashSet<Passenger> Passengers { get { return _passengers; } }

        //Constructors -------------------------------------------------------
        public Ferry() { }
        public Ferry(int id, string name, int carCap, int passengerCap) {
            _id = id;
            _name = name;
            _carCapacity = carCap;
            _passengerCapacity = passengerCap;
        }

        //Methods-------------------------------------------------------------
        public Car AddCar (Car car)
        {
            if (_cars.Add(car))
            {
                return car;
            } else
            {
                return null;
            }
        }

        public Car RemoveCar (Car car)
        {
            if (_cars.Remove(car))
            {
                return car;
            } else
            {
                return null;
            }
        }

        public Passenger AddPassenger (Passenger passenger)
        {
            if (_passengers.Add(passenger))
            {
                return passenger;
            } else
            {
                return null;
            }
        }

        public Passenger RemovePassenger (Passenger passenger)
        {

            if (_passengers.Remove(passenger)) {
                return passenger;
            } else
            {
                return null;
            }
        }
    }
}
