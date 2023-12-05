using Data_Transfer_Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
