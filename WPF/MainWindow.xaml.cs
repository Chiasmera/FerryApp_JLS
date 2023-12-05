using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
        HashSet<Passenger> passengers = new HashSet<Passenger>();

        public MainWindow()
        {
            InitializeComponent();

            currentCar = new Car(1,"testreg",965.00);
            carGrid.DataContext = currentCar;
            bookingPanel.Visibility = Visibility.Collapsed;
            passengerListBox.DataContext = passengers;

            passengers.Add(new Passenger(1, "Testy McTestyson", "male"));
        }

        private void ferryListView_Loaded(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            Task<string> task = client.GetStringAsync(Endpoints.FERRIES_ALL);
            string body = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true,
            };

            List<Ferry> ferries = JsonSerializer.Deserialize<List<Ferry>>( body , option);

            ferryListView.ItemsSource = ferries;
        }

        private void addToCarByID_Click(object sender, RoutedEventArgs e)
        {
            FindByIDWindow window = new FindByIDWindow();
            window.Show();
        }

        private void addNewToCar_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassengerWindow window = new AddNewPassengerWindow();
            window.Show();


            //currentCar.Passengers.Add();
        }

        private void ferryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

    }
}
