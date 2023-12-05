using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Model
{
    internal class Car
    {
        //Fields -------------------------------------------------------------
        private int _id;
        private string _registration;
        private double _weight;

        public int Id { get { return _id; } set { _id = value; } }
        public string Registration { get { return _registration; } set {  _registration = value; } }
        public double Weight { get { return _weight; } set { _weight = value; } }


        //Linkattributes--------------------------
        private HashSet<Passenger> _passengers = new HashSet<Passenger>();
        public HashSet<Passenger> Passengers { get { return _passengers; } }
        private int _driverID;
        public int DriverID { get { return _driverID; } set { _driverID = value; } }

        //Constructors -------------------------------------------------------
        public Car() { }
        public Car(int id, string registration, double weight)
        {
            _id = id;
            _registration = registration;
            _weight = weight;
        }


        //Methods-------------------------------------------------------------
        public Passenger AddPassenger(Passenger passenger)
        {
            _passengers.Add(passenger);
            return passenger;
        }

        public Passenger RemovePassenger(Passenger passenger)
        {
            _passengers.Remove(passenger);
            return passenger;
        }

    }
}
