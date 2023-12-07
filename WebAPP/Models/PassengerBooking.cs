using Data_Transfer_Objects.Model;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models
{
    public class PassengerBooking
    {

        public int ferryID { get; set; }

        [Display(Name = "ID")]
        public int passengerID { get; set; }

        [Display(Name ="Name")]
        public string passengerName { get; set; }

        [Display(Name = "Gender")]
        public string passengerGender { get; set; }
    }
}
