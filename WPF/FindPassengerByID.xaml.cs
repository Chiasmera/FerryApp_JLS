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
            Passenger recieved = APIGet<Passenger>(Endpoints.PASSENGER + passenger.Id);
            if (recieved != null)
            {
                passenger = recieved;
                this.DataContext = passenger;
            } else
            {
                MessageBox.Show("Ingen passager fundet med det ID");
            }
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
    }
}
