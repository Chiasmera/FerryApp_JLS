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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ferry selectedFerry = null;
        Car currentCar;
        ObservableCollection<Passenger> passengers = new ObservableCollection<Passenger>();

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

            if (window.passenger != null
                && window.passenger.Name.Length > 1
                && window.passenger.Id > 0)
            { passengers.Add(window.passenger); }

        }

        private void addNewToCar_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassengerWindow window = new AddNewPassengerWindow();
            window.ShowDialog();


            if (window.passenger != null
                && window.passenger.Name.Length > 1
                && window.passenger.Id > 0)
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
            } else
            {
                bookingPanel.Visibility = Visibility.Collapsed;
            }

        }

        private void bookCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentCar.Registration.Trim().Length == 0
                || currentCar.Weight < 1
                || currentCar.DriverID < 1
                || selectedFerry == null
                || selectedFerry.Id < 1
                ) { MessageBox.Show("Sigende fejlbesked"); }
            else
            {
                Car carToBook = new Car(0, currentCar.Registration, currentCar.Weight);
                foreach (Passenger p in passengers)
                {
                    carToBook.Passengers.Add(p.Id);
                }
                carToBook.DriverID = currentCar.DriverID;

                Car recievedCar = APIPost(Endpoints.ADDCAR, carToBook);

                if (recievedCar != null && recievedCar.Id > 0)
                {
                    Car bookedCarResponse = APIPut(Endpoints.FERRY + $"{selectedFerry.Id}/Add/Car/{recievedCar.Id}", carToBook);
                    if (bookedCarResponse != null)
                    {
                        MessageBox.Show("Booking lykkedes!");
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

            BookPassengerToFerry(window.passenger);

        }

        private void bookWalkingNew_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassengerWindow window = new AddNewPassengerWindow();
            window.ShowDialog();

            BookPassengerToFerry(window.passenger);
        }

        private void BookPassengerToFerry(Passenger passenger)
        {
            if (passenger != null
                 && passenger.Name.Length > 1
                 && passenger.Id > 0)
            {
                Passenger bookedPassengerResponse = APIPut(Endpoints.FERRY + $"{selectedFerry.Id}/Add/Passenger/{passenger.Id}", passenger);
                if (bookedPassengerResponse != null)
                {
                    MessageBox.Show("Booking lykkedes!");
                }
                UpdateFerryView();
            }
        }

        private void UpdateFerryView ()
        {
            ferryListView.ItemsSource = APIGet<ObservableCollection<Ferry>>(Endpoints.FERRY);
        }

        private T APIGet<T>(string URLendpoint)
        {
            HttpClient client = new HttpClient();

            try
            {
                Task<string> task = client.GetStringAsync(URLendpoint);
                string body = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true,
                };
                return JsonSerializer.Deserialize<T>(body, option);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private T APIPost<T>(string URLendpoint, T ferryAPIObject)
        {

            try
            {
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> task = client.PostAsJsonAsync<T>(URLendpoint, ferryAPIObject);
                HttpResponseMessage result = task.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IncludeFields = true,
                    };
                    return JsonSerializer.Deserialize<T>(result.Content.ReadAsStream(), option);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private T APIPut<T>(string URLendpoint, T ferryAPIObject)
        {

            try
            {
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> task = client.PutAsJsonAsync<T>(URLendpoint, ferryAPIObject);
                HttpResponseMessage result = task.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IncludeFields = true,
                    };
                    return JsonSerializer.Deserialize<T>(result.Content.ReadAsStream(), option);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
