using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects.Model
{
    public class Car
    {
        //Fields -------------------------------------------------------------
        private int _id;
        private string _registration;
        private double _weight;

        public int Id { get { return _id; } set { _id = value; } }
        public string Registration { get { return _registration; } set {  _registration = value; } }
        public double Weight { get { return _weight; } set { _weight = value; } }


        //Linkattributes--------------------------
        private HashSet<int> _passengerIDs = new HashSet<int>();
        public HashSet<int> Passengers { get { return _passengerIDs; } set { _passengerIDs = value; } }
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
        public int AddPassenger(int passengerID)
        {
            if(_passengerIDs.Add(passengerID))
            {
                return passengerID;
            } else
            {
                return -1;
            }
        }

        public int RemovePassenger(int passengerID)
        {
            if (_passengerIDs.Remove(passengerID))
            {
                return passengerID;
            }
            else
            {
                return -1;
            }
        }
    }
}
