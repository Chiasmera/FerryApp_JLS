﻿using Data_Transfer_Objects.Model;
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
    /// Interaction logic for AddNewPassengerWindow.xaml
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
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true,
            };

            HttpClient client = new HttpClient();

            Task<HttpResponseMessage> task = client.PostAsJsonAsync<Passenger>(Endpoints.ADDPASSENGER, passenger);
            HttpResponseMessage result = task.Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                passenger = JsonSerializer.Deserialize<Passenger>(result.Content.ReadAsStream(), option);
            }

            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            passenger = null;
            this.Close();
        }
    }
}
