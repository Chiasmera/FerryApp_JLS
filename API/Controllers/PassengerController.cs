using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        [Route("{id}")]
        [HttpGet]
        public Passenger GetByID(int id)
        {
            PassengerBL passengerBL  = new PassengerBL();

            return passengerBL.GetByID(id);
        }

        [Route("Add")]
        [HttpPost]
        public Passenger Add(Passenger passenger)
        {
            PassengerBL passengerBL = new PassengerBL();
            return passengerBL.Add(passenger);
        }

        [Route("Update")]
        [HttpPut]
        public Passenger Update(Passenger passenger)
        {
            PassengerBL passengerBL = new PassengerBL();

            return passengerBL.Update(passenger);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        public Passenger Remove(int id)
        {
            PassengerBL passengerBL = new PassengerBL();
            return passengerBL.Remove(id);
        }
    }
}
