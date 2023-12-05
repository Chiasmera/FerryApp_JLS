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
        public MainWindow()
        {
            InitializeComponent();
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

            
            textBox.Text = body;

            ferryListView.ItemsSource = ferries;

            
        }
    }
}
