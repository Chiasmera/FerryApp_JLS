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
        private double _passengerPrice;
        private double _carPrice;

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public int CarCapacity { get { return _carCapacity; } set { _carCapacity = value; } }
        public int PassengerCapacity { get { return _passengerCapacity; } set { _passengerCapacity = value; } }
        public double PassengerPrice { get { return _passengerPrice; } set { _passengerPrice = value; } }
        public double CarPrice { get { return _carPrice; } set { _carPrice = value; } }

        //Linkattributes--------------------------
        private HashSet<Car> _cars = new HashSet<Car>();
        public HashSet<Car> Cars { get { return _cars; } }

        private HashSet<Passenger> _passengers = new HashSet<Passenger>();
        public HashSet<Passenger> Passengers { get { return _passengers; } }

        //Constructors -------------------------------------------------------
        public Ferry() { }
        public Ferry(int id, string name, int carCap, int passengerCap, double passengerPrice = 99.00, double carPrice = 197.00)
        {
            _id = id;
            _name = name;
            _carCapacity = carCap;
            _passengerCapacity = passengerCap;
            PassengerPrice = passengerPrice;
            CarPrice = carPrice;
        }

        //Methods-------------------------------------------------------------
        public Car AddCar (Car car)
        {
            //checks if a passenger in the car is already present on the ferry
            foreach (Passenger passenger in car.Passengers)
            {
                if (_passengers.Contains(passenger))
                {
                    //If so, removes the passenger from the list of non-car passengers
                    _passengers.Remove(passenger);
                }
            }

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

            //checks if the passenger is already present in a car
            foreach (Car car in _cars)
            {
                if (car.Passengers.Contains(passenger))
                {
                    if (car.DriverID == passenger.Id)
                    {
                        //If passenger is the driver of a car, the method fails.
                        return null;
                    } else
                    {
                        //Otherwise removes the passenger from the car
                        car.RemovePassenger(passenger);
                    }

                }
            }

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

        public double GetTotalPrice()
        {
            double sum = 0.0;
            foreach (Car car in _cars)
            {
                sum += CarPrice;
                sum += (car.Passengers.Count * PassengerPrice);
                Console.WriteLine(car.Passengers.Count);
            }
            sum += (Passengers.Count * PassengerPrice);
            return sum;
        }
    }
}
