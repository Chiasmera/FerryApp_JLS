using Data_Transfer_Objects.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Main window of the booking management system
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ferry selectedFerry = null;
        private Car currentCar;
        private ObservableCollection<Passenger> passengers = new ObservableCollection<Passenger>();

        public MainWindow()
        {
            InitializeComponent();
            currentCar = new Car();
            carGrid.DataContext = currentCar;
            bookingPanel.Visibility = Visibility.Collapsed;
            passengerListView.ItemsSource = passengers;
        }

        private void ferryListView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFerryView();
        }

        private void addToCarByID_Click(object sender, RoutedEventArgs e)
        {
            FindPassengerByID window = new FindPassengerByID();
            window.ShowDialog();

            if (window.passenger != null)
            {
                passengers.Add(window.passenger);
            }

        }

        private void addNewToCar_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassengerWindow window = new AddNewPassengerWindow();
            window.ShowDialog();


            if (window.passenger != null)
            { passengers.Add(window.passenger); }

        }

        private void ferryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ferryListView.SelectedItems.Count > 0)
            {
                selectedFerry = ferryListView.SelectedItems[0] as Ferry;
                if (selectedFerry == null)
                {
                    bookingPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    bookingPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                bookingPanel.Visibility = Visibility.Collapsed;
            }

        }

        private void bookCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateCarPassengers(passengers) && ValidateCar(currentCar) && ValidateDriverIsInCar(passengers, currentCar.DriverID))
            {
                Car carToBook = new Car(0, currentCar.Registration, currentCar.Weight);
                foreach (Passenger p in passengers)
                {
                    carToBook.Passengers.Add(p.Id);
                }
                carToBook.DriverID = currentCar.DriverID;

                Car recievedCar = FerryAPIServices.APIPost(Endpoints.ADDCAR, carToBook);

                if (recievedCar != null && recievedCar.Id > 0)
                {
                    Car bookedCarResponse = FerryAPIServices.APIPut(Endpoints.FERRY + $"{selectedFerry.Id}/Add/Car/{recievedCar.Id}", carToBook);
                    if (bookedCarResponse != null)
                    {
                        MessageBox.Show("Booking succeeded", "Success!");
                        currentCar = new Car();
                        carGrid.DataContext = currentCar;
                        passengerListView.ItemsSource = passengers;
                    }
                }

                UpdateFerryView();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var item = (sender as RadioButton).DataContext;
            if (item != null)
            {
                int index = passengerListView.Items.IndexOf(item);
                currentCar.DriverID = passengers[index].Id;

                testLabel.Content = currentCar.DriverID;
            }
        }

        private void bookWalkingByID_Click(object sender, RoutedEventArgs e)
        {
            FindPassengerByID window = new FindPassengerByID();
            window.ShowDialog();

            if (window.passenger != null)
            {
                BookPassengerToFerry(window.passenger);
            }
        }

        private void bookWalkingNew_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassengerWindow window = new AddNewPassengerWindow();
            window.ShowDialog();

            if (window.passenger != null)
            {
                BookPassengerToFerry(window.passenger);
            }
        }

        /// <summary>
        /// Attempts to add the passenger to the ferry, by utilizing the FerryAPI
        /// </summary>
        /// <param name="passenger">passenger to add</param>
        private void BookPassengerToFerry(Passenger passenger)
        {
            if (passenger != null
                 && passenger.Name.Length > 1
                 && passenger.Id > 0)
            {
                Passenger bookedPassengerResponse = FerryAPIServices.APIPut(Endpoints.FERRY + $"{selectedFerry.Id}/Add/Passenger/{passenger.Id}", passenger);
                if (bookedPassengerResponse != null)
                {
                    MessageBox.Show($"{bookedPassengerResponse.Name} has been added as a passenger", "Booking succeeded!");
                }
                UpdateFerryView();
            }
        }

        /// <summary>
        /// Manually refreshes the ferry view, by fetching the full list of ferries from the API
        /// </summary>
        private void UpdateFerryView()
        {
            ferryListView.ItemsSource = FerryAPIServices.APIGet<ObservableCollection<Ferry>>(Endpoints.FERRY);
        }

        private bool ValidateCar(Car car)
        {
            if (car == null) { return false; }
            else if(car.Registration == null || car.Registration.Length < 1)
            {
                MessageBox.Show("Registration cannot be empty", "Registration required");
            } else if (car.Weight == null || car.Weight < 1)
            {
                MessageBox.Show("Please input the cars weight. It must be a positive number.", "Weight required");
            } else if (car.DriverID == null)
            {
                MessageBox.Show("Please select which passenger is driving the car. A car must have a driver.", "Driver required");
            }  else 
            {
                return true;
            }
            return false;
        }

        private bool ValidateCarPassengers (Collection<Passenger> passengers)
        {
            if (passengers == null || passengers.Count < 1)
            {
                MessageBox.Show("A car must have at least one passenger (the driver)", "No Passengers");
            }
            else if (passengers.Count > 5)
            {
                MessageBox.Show("A car cannot have more than 5 passengers. Please remove passengers.", "Too many passengers");
            } else
            {
                return true;
            }
            return false;
        }

        private bool ValidateDriverIsInCar (Collection<Passenger> passengers, int driverID)
        {
            if (driverID == 0)
            {
                MessageBox.Show("The car has no driver. Please set a driver of the car.", "No Driver");
            } else
            {
                foreach (Passenger p in passengers)
                {
                    if (p.Id == driverID)
                    {
                        return true;
                    }
                }
                MessageBox.Show("The car has no driver. Please set a driver of the car.", "No Driver");
            }
            return false;
        }

    }
}
