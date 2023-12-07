using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models
{
    public class FerryViewModel
    {
        private int _id;
        private string _name;
        private int _carCapacity;
        private int _passengerCapacity;
        private double _passengerPrice;
        private double _carPrice;

        [Range(0, 99999)]
        public int Id { get { return _id; } set { _id = value; } }

        [Required(ErrorMessage = "You forgot to enter your name")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get { return _name; } set { _name = value; } }

        [Display(Name ="Maximum Car Capacity")]
        [Required(ErrorMessage ="You must set a maximum car capacity")]
        [Range(10, 99999)]
        public int CarCapacity { get { return _carCapacity; } set { _carCapacity = value; } }

        [Display(Name = "Maximum Passenger Capacity")]
        [Required(ErrorMessage = "You must set a maximum passenger capacity")]
        [Range(40,99999)]
        public int PassengerCapacity { get { return _passengerCapacity; } set { _passengerCapacity = value; } }


        [Display(Name = "Price per Passenger")]
        [Range(0, double.MaxValue)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression("^\\$?(\\d{1,3},?(\\d{3},?)*\\d{3}(\\.\\d{1,3})?|\\d{1,3}(\\.\\d{2})?)$", ErrorMessage = "Price must be a number with a dot seperating decimals")]
        public double PassengerPrice { get { return _passengerPrice; } set { _passengerPrice = value; } }

        [Display(Name = "Price per Car")]
        [Range(0.00, double.MaxValue)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression("^\\$?(\\d{1,3},?(\\d{3},?)*\\d{3}(\\.\\d{1,3})?|\\d{1,3}(\\.\\d{2})?)$", ErrorMessage ="Price must be a number with a dot seperating decimals")]
        public double CarPrice { get { return _carPrice; } set { _carPrice = value; } }
        }
}
