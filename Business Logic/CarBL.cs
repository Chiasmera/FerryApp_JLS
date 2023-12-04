using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    internal class CarBL
    {
        public Car GetByID(int id)
        {
            if (id < 1) { return null; }
            return CarRepository.GetByID(id);
        }


        public Car Add(Car car)
        {
            if (car == null) { return null; }
            return CarRepository.Add(car);
        }

        public Car Update(Car car)
        {
            if (car == null) { return null; }
            return CarRepository.Update(car);
        }

        public Car Remove(int carID)
        {
            if (carID < 1) { return null; }
            return CarRepository.Remove(carID);
        }
    }
}
