using Data_Access.Repository;
using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    /// <summary>
    /// Responsible for Business Logic regarding the management of ferries in the system
    /// </summary>
    public class FerryBL
    {
        /// <summary>
        /// Retrieves all Ferries in the database
        /// </summary>
        /// <returns>A collection of all Ferries</returns>
        public HashSet<Ferry> GetAll()
        {
            return FerryRepository.GetAll();
        }

        /// <summary>
        /// Retrieves a specific ferry by its ID
        /// </summary>
        /// <param name="id">The id for the Ferry</param>
        /// <returns>the ferry with the provided ID if succcessfull, null otherwise</returns>
        public Ferry Get(int id)
        {
            if (id < 1) { return null; }
            return FerryRepository.Get(id);
        }

        /// <summary>
        /// Adds a ferry to the database
        /// </summary>
        /// <param name="ferry">The Ferry to add</param>
        /// <returns>the added Ferry if successfull, null otherwise</returns>
        public Ferry Add (Ferry ferry)
        {
            if (
                ferry == null
                || ferry.Name.Length < 2
                || ferry.PassengerCapacity < 40
                || ferry.CarCapacity < 10
                || ferry.CarPrice < 0
                || ferry.PassengerPrice < 0
                )
            {
                return null;
            }
            return FerryRepository.Add(ferry);
        }

        /// <summary>
        /// Updates a ferry in the database to match the provided Ferry
        /// </summary>
        /// <param name="ferry">A Ferry with the new values, and ID to match the ferry to update</param>
        /// <returns>the updated ferry if successfull, null otherwise</returns>
        public Ferry Update(Ferry ferry)
        {
            if (
                ferry == null 
                || ferry.Name.Length < 2
                || ferry.PassengerCapacity < 40
                || ferry.CarCapacity < 10
                || ferry.CarPrice < 0
                || ferry.PassengerPrice < 0
                ) { 
                return null; 
            }
            return FerryRepository.Update(ferry);
        }

        public Ferry Remove(int id)
        {
            if (id < 1) { return null; }
            return FerryRepository.Remove(id);
        }

        /// <summary>
        /// Adds a walking passenger to a ferry, removing it from a car on the ferry if present and not driver.
        /// </summary>
        /// <param name="ferryID">The ID of the ferry the passenger should be addded to</param>
        /// <param name="passengerID">the ID of the passenger to add</param>
        /// <returns>the added passenger if successfull, null othwerwise</returns>
        public Passenger AddPassenger(int ferryID, int passengerID)
        {
            if (ferryID < 0 || passengerID < 0) { return null; }
            IFerryable found = FerryRepository.HasPassenger(ferryID, passengerID);
            if (found != null && found is Car)
            {
                Car car = (Car)found;
                if (car.DriverID == passengerID)
                {
                    return null;
                } else
                {
                    car.Passengers.Remove(passengerID);
                }
            }
            return FerryRepository.AddPassenger(ferryID, passengerID);
        }



        /// <summary>
        /// Removes a specific walking passenger from a ferry
        /// </summary>
        /// <param name="ferryID">The ID of the ferry to remove the passenger from</param>
        /// <param name="passengerID">The ID of the passenger to remove</param>
        /// <returns>the id of the removed passenger, null otherwise</returns>
        public Passenger RemovePassenger(int ferryID, int passengerID)
        {
            if (ferryID < 0 || passengerID < 0) { return null; }
            return FerryRepository.RemovePassenger(ferryID, passengerID);
        }

        /// <summary>
        /// Adds a car, including its passengers, to a ferry, removing any included passengers from the walking passengers on the ferry
        /// </summary>
        /// <param name="ferryID">The ID of the ferry to which the car should be added</param>
        /// <param name="carID">The ID of the car to add</param>
        /// <returns>the added Car if successfull, null otherwise</returns>
        public Car AddCar(int ferryID, int carID)
        {
            if (ferryID < 0 || carID < 0) { return null; }

            Car car = CarRepository.GetByID(carID);
            if (car == null) { return null; }

            //Database consistency will be slightly compromised with this approach, as it only ensures consistensy for the given ferry.
            //A passenger will not be removed as walking from another ferry than the current (which they should in the current state of the system)
            //I am not prioritizing fixing this issue, as database integrity and handling is not a focus for this assignment,
            //and the database is suboptimal for handling booking consistency on more then one ferry
            //the system should really be designed to handle different bookings/departures for ferries, and the passenger and/or car should be assigned to a that class instead
            //This is out of scope for the assignment though.

                HashSet<int> removeFromCar = new HashSet<int>();
                HashSet<int> removeFromPassengers = new HashSet<int>();
                foreach (int pID in car.Passengers)
                {
                    IFerryable found = FerryRepository.HasPassenger(ferryID, pID);
                    if (found != null)
                    {
                        if (found is Car)
                        {
                            Car foundCar = (Car)found;
                            if (foundCar.DriverID == pID)
                            {
                                return null;
                            }
                            removeFromCar.Add(pID);
                        }
                        else if (found is Passenger)
                        {
                            removeFromPassengers.Add(pID);
                        }
                    }
                }

                CarRepository.RemovePassengers(carID, removeFromCar);
                foreach (int id in removeFromPassengers)
                {
                    FerryRepository.RemovePassenger(ferryID, id);
                }
            

            return FerryRepository.AddCar(ferryID, carID);
        }

        /// <summary>
        /// Removes a specific car and its passengers from the ferry
        /// </summary>
        /// <param name="ferryID">the ID of the ferry where the car should be removed</param>
        /// <param name="carID">the Id of the car to remove</param>
        /// <returns>the removed Car if successfull, null otherwise</returns>
        public Car RemoveCar(int ferryID, int carID)
        {
            if (ferryID < 0 || carID < 0) { return null; }
            return FerryRepository.RemoveCar(ferryID, carID);
        }

        /// <summary>
        /// ONLY FOR TESTING. Clears the database and adds the test-set of data.
        /// </summary>
        public void ResetTestData ()
        {
            FerryRepository.TESTClearAllData();
            Ferry addedF1 = FerryRepository.Add(new Ferry(0, "Lilleput", 10, 40, 197.00, 99.00));
            Ferry addedF2 = FerryRepository.Add(new Ferry(0, "Gargantua", 20, 80, 197.00, 99.00));

            Passenger p1 = new Passenger(0, "Hans", "male");
            Passenger p2 = new Passenger(0, "Jørgen", "male");
            Passenger p3 = new Passenger(0, "Jens", "male");
            Passenger p4 = new Passenger(0, "Andreas", "male");
            Passenger p5 = new Passenger(0, "Bent", "male");
            Passenger p6 = new Passenger(0, "Grethe", "female");
            Passenger p7 = new Passenger(0, "Ida", "female");
            Passenger p8 = new Passenger(0, "Amanda", "female");
            Passenger p9 = new Passenger(0, "Katrine", "female");
            Passenger p10 = new Passenger(0, "Ulla", "female");
            Passenger adddedP1 = PassengerRepository.Add(p1);
            Passenger adddedP2 = PassengerRepository.Add(p2);
            Passenger adddedP3 = PassengerRepository.Add(p3);
            Passenger adddedP4 = PassengerRepository.Add(p4);
            Passenger adddedP5 = PassengerRepository.Add(p5);
            Passenger adddedP6 = PassengerRepository.Add(p6);
            Passenger adddedP7 = PassengerRepository.Add(p7);
            Passenger adddedP8 = PassengerRepository.Add(p8);
            Passenger adddedP9 = PassengerRepository.Add(p9);
            Passenger adddedP10 = PassengerRepository.Add(p10);

            Car car1 = new Car(0, "BK95661", 854);
            car1.DriverID = adddedP1.Id;
            car1.Passengers.Add(adddedP1.Id);
            car1.Passengers.Add(adddedP2.Id);
            car1.Passengers.Add(adddedP5.Id);
            car1.Passengers.Add(adddedP7.Id);

            Car car2 = new Car(0, "EX73992", 1264);
            car2.DriverID = adddedP3.Id;
            car2.Passengers.Add(adddedP3.Id);
            car2.Passengers.Add(adddedP10.Id);

            Car addedC1 = CarRepository.Add(car1);
            Car addedC2 = CarRepository.Add(car2);

            FerryRepository.AddCar(addedF1.Id, addedC1.Id);
            FerryRepository.AddCar(addedF2.Id, addedC2.Id);

            FerryRepository.AddPassenger(addedF1.Id, adddedP4.Id);
            FerryRepository.AddPassenger(addedF1.Id, adddedP6.Id);
            FerryRepository.AddPassenger(addedF1.Id, adddedP8.Id);
            FerryRepository.AddPassenger(addedF2.Id, adddedP9.Id);
        }

    }
}
