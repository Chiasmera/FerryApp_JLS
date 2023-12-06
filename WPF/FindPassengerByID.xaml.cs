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
    /// Interaction logic for FindPassengerByID.xaml
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
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true,
            };

            HttpClient client = new HttpClient();

            Task<string> task = client.GetStringAsync(Endpoints.PASSENGER + passenger.Id);
            string result = task.Result;
            try
            {
                passenger = JsonSerializer.Deserialize<Passenger>(result, option);
                this.DataContext = passenger;
            } catch (Exception ex) { MessageBox.Show("Ingen passager fundet med det ID"); }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            passenger = null;
            this.Close();
        }
    }
}
