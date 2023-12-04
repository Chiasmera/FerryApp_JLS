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



        //Constructors -------------------------------------------------------
        public Car() { }
        public Car(int id, string registration, double weight)
        {
            _id = id;
            _registration = registration;
            _weight = weight;
        }


        //Methods-------------------------------------------------------------

    }
}
