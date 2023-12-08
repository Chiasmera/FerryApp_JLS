using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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
    /// Window for finding a Passenger by its ID
    /// </summary>
    public partial class FindPassengerByID : Window
    {
        public Passenger passenger;
        public FindPassengerByID()
        {
            InitializeComponent();
            passenger = new Passenger();
            this.DataContext = passenger;
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
            Passenger recieved = FerryAPIServices.APIGet<Passenger>(Endpoints.PASSENGER + passenger.Id);
            if (recieved != null)
            {
                passenger = recieved;
                this.DataContext = passenger;
            } else
            {
                MessageBox.Show("No passenger found with that ID!");
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidity(passenger))
            {
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
            }
            else if (passenger.Gender == null || passenger.Gender.Length < 1)
            {
                MessageBox.Show("You must specify a gender (But it can be anything)", "No Gender");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
