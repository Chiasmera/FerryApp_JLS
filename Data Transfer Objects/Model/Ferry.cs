using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects.Model
{
    public class Ferry
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

        //Computed Properties---------------------
        public double TotalIncome
        {
            get
            {
                double totalIncome = 0;
                foreach (Car car in _cars)
                {
                    totalIncome += car.Passengers.Count * _passengerPrice + _carPrice;
                }
                totalIncome += _passengers.Count * _passengerPrice;
                return totalIncome;
            }
        }

        public double TotalPassengers
        {
            get
            {
                int count = Passengers.Count;
                foreach (Car car in _cars)
                {
                    count += car.Passengers.Count;
                }
                return count;

            }
        }

        //Linkattributes--------------------------
        private HashSet<Car> _cars = new HashSet<Car>();
        public HashSet<Car> Cars { get { return _cars; } set { _cars = value; } }

        private HashSet<int> _passengers = new HashSet<int>();
        public HashSet<int> Passengers { get { return _passengers; } set { _passengers = value; } }

        //Constructors -------------------------------------------------------
        public Ferry() { }
        public Ferry(int id, string name, int capCap, int passengerCap, double carPrice, double passengerPrice)
        {
            _id = id;
            _name = name;
            _carCapacity = capCap;
            _passengerCapacity = passengerCap;
            _passengerPrice = passengerPrice;
            _carPrice = carPrice;
        }

        //Methods-------------------------------------------------------------


    }
}
