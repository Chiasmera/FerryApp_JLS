using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FerryController : ControllerBase
    {
        [HttpGet]
        public HashSet<Ferry>  GetAll()
        {
            FerryBL ferryBL = new FerryBL();
            return ferryBL.GetAll();
        }

        [Route("{id}")]
        [HttpGet]
        public Ferry GetByID( int id)
        {
            FerryBL ferryBL = new FerryBL();

            return ferryBL.GetByID(id);
        }

        [Route("Add")]
        [HttpPost]
        public void Add(Ferry ferry)
        {
            FerryBL ferryBL = new FerryBL();
            ferryBL.Add(ferry);
        }

        [Route("Update")]
        [HttpPut]
        public void Update(Ferry ferry)
        {
            FerryBL ferryBL = new FerryBL();
            ferryBL.Update(ferry);
        }

        [Route("{ferryID}/Add/Passenger/{passengerID}")]
        [HttpPut]
        public Passenger AddPassenger(int ferryID, int passengerID)
        {
            FerryBL ferryBL = new FerryBL();
            return ferryBL.AddPassenger(ferryID, passengerID);
        }

        [Route("{ferryID}/Remove/Passenger/{passengerID}")]
        [HttpDelete]
        public Passenger RemovePassenger(int ferryID, int passengerID)
        {
            FerryBL ferryBL = new FerryBL();
            return ferryBL.RemovePassenger(ferryID, passengerID);
        }

        [Route("{ferryID}/Add/Car/{carID}")]
        [HttpPut]
        public Car AddCar(int ferryID, int carID)
        {
            FerryBL ferryBL = new FerryBL();
            return ferryBL.AddCar(ferryID, carID);
        }

        [Route("{ferryID}/Remove/Car/{carID}")]
        [HttpPut]
        public Car RemoveCar(int ferryID, int carID)
        {
            FerryBL ferryBL = new FerryBL();
            return ferryBL.RemoveCar(ferryID, carID);
        }


    }

}
