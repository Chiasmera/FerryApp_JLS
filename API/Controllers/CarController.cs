using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [Route("{id}")]
        [HttpGet]
        public Car GetByID(int id)
        {
            CarBL carBL = new CarBL();

            return carBL.GetByID(id);
        }

        [Route("Add")]
        [HttpPost]
        public Car Add(Car car)
        {
            CarBL carBL = new CarBL();
            return carBL.Add(car);
        }

        [Route("Update")]
        [HttpPut]
        public Car Update(Car car)
        {
            CarBL carBL = new CarBL();

            return carBL.Update(car);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        public Car Remove(int id)
        {
            CarBL carBL = new CarBL();
            return carBL.Remove(id);
        }

        [Route("{carID}/Add/Passengers")]
        [HttpPut]
        public HashSet<Passenger> AddPassengers(int carID, HashSet<int> passengers)
        {
            CarBL carBL = new CarBL();
            return carBL.AddPassengers(carID, passengers);
        }
    }
}
