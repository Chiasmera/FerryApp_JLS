﻿using Business_Logic;
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
            if (action == null || !idGood)
            {
                ViewBag.error = "There was an error with the selected action. Try again";
                FerryBL ferryBL = new FerryBL();
                HashSet<Ferry> ferries = ferryBL.GetAll();
                return RedirectToAction("Index", "Home");

            }
            else
            {
                if (action.Equals("Book Car"))
                {
                    Car car = new Car();
                    return View("BookCar", car);
                }
                else if (action.Equals("Book Passenger"))
                {

                    PassengerBooking passengerBooking = new PassengerBooking();
                    passengerBooking.ferryID = ferryID;
                    return View("BookPassenger", passengerBooking);
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
            } else
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
    }
}
