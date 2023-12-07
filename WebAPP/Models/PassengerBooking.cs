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
        //[Required(ErrorMessage = "You forgot to enter your name")]
        //[StringLength(120, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters")]
        public string passengerName { get; set; }

        [Display(Name = "Gender")]
        //[Required(ErrorMessage = "You forgot to enter your name")]
        //[StringLength(120, MinimumLength = 2, ErrorMessage = "Gender must be at least 2 characters")]
        public string passengerGender { get; set; }
    }
}
