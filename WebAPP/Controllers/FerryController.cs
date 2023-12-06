using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
    public class FerryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnActionSubmit(IFormCollection form)
        {
            string action = form["ferryAction"];
            int ferryID = 0;
            bool idGood = Int32.TryParse( form["ferryList"], out ferryID);
            if ( action == null || !idGood)
            {
                ViewBag.error = "There was an error with the selected action. Try again";
                FerryBL ferryBL = new FerryBL();
                HashSet<Ferry> ferries = ferryBL.GetAll();
                return RedirectToAction("Index", "Home");

            } else {
                if (action.Equals("Book Car"))
                {
                    Car car = new Car();
                    return View("BookCar", car);
                }
                else if (action.Equals("Book Passenger"))
                {
                    Passenger passenger = new Passenger();
                    return View("BookPassenger", passenger);
                }
                else
                {
                    ViewBag.error = "There was an error with the selected action. Try again";
                    FerryBL ferryBL = new FerryBL();
                    HashSet<Ferry> ferries = ferryBL.GetAll();
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}
