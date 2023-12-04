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

        //Hovedsageligt til oprettelse af testdata. Program har ikke indenfor scope at lave CRUD på færger
        [Route("Add")]
        [HttpPost]
        public void AddFerry(Ferry ferry)
        {
            FerryBL ferryBL = new FerryBL();
            ferryBL.Add(ferry);
        }


        //[Route("/{id}/Cars")]
        //[HttpGet]
        //public HashSet<Car> GetCarsForFerry(int id)
        //{
        //    FerryBL ferryBL = new FerryBL();
        //    return ferryBL.GetCarsForFerry(id);
        //}

        //[Route("/{ferryID}/Add/Car")]
        //[HttpPost]
        //public void AddCarToFerry(int ferryID, Car car, int driverID)
        //{
        //    FerryBL ferryBL = new FerryBL();
        //    ferryBL.AddCarToFerry(ferryID, car, driverID);
        //}




    }

}
