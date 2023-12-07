using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Model
{
    /// <summary>
    /// Models a booking af a car on a Ferry.
    /// </summary>
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
        /// <summary>
        /// Adds a passenger to this car
        /// </summary>
        /// <param name="passenger">the Passenger to add</param>
        /// <returns>the added Passenger if successfull, null otherwise</returns>
        public Passenger AddPassenger(Passenger passenger)
        {
            if (_passengers.Add(passenger))
            {
            return passenger;
            } 
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Removes a passenger from this car
        /// </summary>
        /// <param name="passenger">The passenger to remove</param>
        /// <returns>the removed passenger if successfull, null otherwise</returns>
        public Passenger RemovePassenger(Passenger passenger)
        {
            _passengers.Remove(passenger);
            return passenger;
        }

    }
}
