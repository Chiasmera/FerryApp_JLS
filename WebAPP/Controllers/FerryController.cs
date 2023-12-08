using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class FerryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OnActionSubmit(IFormCollection form)
        {
            string action = form["ferryAction"];
            int ferryID = 0;
            bool idGood = Int32.TryParse(form["ferryList"], out ferryID);
            if (action == null)
            {
                ViewBag.error = "There was an error with the selected action. Try again";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                FerryBL ferryBL = new FerryBL();

                if (action.Equals("Book Passenger"))
                {
                    PassengerBooking passengerBooking = new PassengerBooking();
                    passengerBooking.ferryID = ferryID;
                    return View("BookPassenger", passengerBooking);
                }
                else if (action.Equals("Add Ferry"))
                {
                    FerryViewModel ferry = new FerryViewModel();
                    ferry.CarCapacity = 10;
                    ferry.PassengerCapacity = 40;
                    ferry.CarPrice = 197.00;
                    ferry.PassengerPrice = 99.00;
                    return View("Add", ferry);
                }
                else if (action.Equals("Remove Ferry"))
                {
                    if (idGood && ferryID != 0) { ferryBL.Remove(ferryID); }
                    
                    return RedirectToAction("Index", "Home");
                }
                else if (action.Equals("Update Ferry"))
                {
                    if (idGood && ferryID != 0) {
                        Console.WriteLine(ferryID);
                        Ferry ferry = ferryBL.Get(ferryID);
                        FerryViewModel ferryVM = new FerryViewModel();
                        ferryVM.Id = ferry.Id;
                        ferryVM.Name = ferry.Name;
                        ferryVM.CarCapacity = ferry.CarCapacity;
                        ferryVM.PassengerCapacity = ferry.PassengerCapacity;
                        ferryVM.CarPrice = ferry.CarPrice;
                        ferryVM.PassengerPrice = ferry.PassengerPrice;
                        return View("Update", ferryVM);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "There was an error with the selected action. Try again";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public IActionResult BookNewPassenger(PassengerBooking booking)
        {
            if (ModelState.IsValid
                && booking.passengerName != null
                && booking.passengerGender != null
                && booking.passengerName.Trim().Length > 1
                && booking.passengerGender.Trim().Length > 1
                && booking.ferryID != 0
                )
            {
                Passenger passenger = new Passenger(0, booking.passengerName, booking.passengerGender);
                PassengerBL passengerBL = new PassengerBL();
                Passenger added = passengerBL.Add(passenger);
                FerryBL ferryBL = new FerryBL();
                Passenger reciept = ferryBL.AddPassenger(booking.ferryID, added.Id);
                return View("PassengerReciept", reciept);
            }
            else
            {
                ViewBag.error = "There was an error with the selected action. Try again";
                FerryBL ferryBL = new FerryBL();
                HashSet<Ferry> ferries = ferryBL.GetAll();
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public IActionResult BookPassengerById(PassengerBooking booking)
        {
            if (
                booking.passengerID > 0
                && booking.ferryID != 0
                )
            {
                FerryBL ferryBl = new FerryBL();
                Passenger reciept = ferryBl.AddPassenger(booking.ferryID, booking.passengerID);
                return View("PassengerReciept", reciept);
            }
            else
            {
                ViewBag.error = "There was an error with the selected action. Try again";
                FerryBL ferryBL = new FerryBL();
                HashSet<Ferry> ferries = ferryBL.GetAll();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Add(FerryViewModel recieved)
        {
            if (ModelState.IsValid && recieved != null)
            {
                FerryBL ferryBL = new FerryBL();
                
                Ferry ferry = ferryBL.Add(new Ferry(0, recieved.Name, recieved.CarCapacity, recieved.PassengerCapacity, recieved.CarPrice, recieved.PassengerPrice));
                if (ferry != null) 
                { 
                    return View("ferryReciept", ferry); 
                } else { 
                    return RedirectToAction("Index", "Home"); 
                }

            } else
            {
                return View("Add");
            }
        }

        [HttpPost]
        public IActionResult Update(FerryViewModel recieved)
        {
            if (ModelState.IsValid && recieved != null)
            {
                FerryBL ferryBL = new FerryBL();

                Ferry ferry = ferryBL.Update(new Ferry(recieved.Id, recieved.Name, recieved.CarCapacity, recieved.PassengerCapacity, recieved.CarPrice, recieved.PassengerPrice));
                if (ferry != null)
                {
                    return View("ferryReciept", ferry);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                return View("Update");
            }
        }
    }
}
