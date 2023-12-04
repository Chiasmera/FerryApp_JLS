using System;
using System.Collections.Generic;
using System.Linq;
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

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public int CarCapacity { get { return _carCapacity; } set { _carCapacity = value; } }
        public int PassengerCapacity { get { return _passengerCapacity; } set { _passengerCapacity = value; } }

        //Linkattributes--------------------------

        //Constructors -------------------------------------------------------
        public Ferry() { }
        public Ferry(int id, string name, int capCap, int passengerCap) {
            _id = id;
            _name = name;
            _carCapacity = capCap;
            _passengerCapacity = passengerCap;
        }

        //Methods-------------------------------------------------------------




    }
}
