using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Model
{
    /// <summary>
    /// Models a passenger on a ferry.
    /// </summary>
    internal class Passenger
    {
        //Fields -------------------------------------------------------------
        private int _id;
        private string _name;
        private string _gender;
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Gender { get { return _gender; } set { _gender = value; } }

        //Linkattributes--------------------------


        //Constructors -------------------------------------------------------
        public Passenger() { }
        public Passenger (int id, string name, string gender)
        {
            _id = id;
            _name = name;
            _gender = gender;
        }


        //Methods-------------------------------------------------------------
    }
}
