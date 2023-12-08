using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Window for adding a new passenger 
    /// </summary>
    public partial class AddNewPassengerWindow : Window
    {
        public Passenger passenger;
        public AddNewPassengerWindow()
        {
            InitializeComponent();
            passenger = new Passenger();
            this.DataContext = passenger;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidity(passenger))
            {
                passenger = FerryAPIServices.APIPost(Endpoints.ADDPASSENGER, passenger);
                this.Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            passenger = null;
            this.Close();
        }

        /// <summary>
        /// Checks the validity of the passenger
        /// </summary>
        /// <param name="passenger">The passenger to check</param>
        /// <returns>true if the passenger is valid, false otherwise</returns>
        private bool CheckValidity(Passenger passenger)
        {
            if (passenger.Name == null || passenger.Name.Length < 2)
            {
                MessageBox.Show("Name must be at least 2 characters", "Name Too Short");
                return false;
            } else if (passenger.Gender == null || passenger.Gender.Length < 1)
            {
                MessageBox.Show("You must specify a gender (But it can be anything)", "No Gender");
                return false;
            } else
            {
                return true;
            }
        }
    }
}
