using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WebApplication1.Controllers;
using WebApplication1.Models.DTO.Input;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AlleBestellingenWindow.xaml
    /// </summary>
    public partial class AlleBestellingenWindow : Window
    {
        private UserController _userControl;
        private ObservableCollection<OrderInputDTO> orders;
        public AlleBestellingenWindow(UserController us)
        {
            InitializeComponent();
            _userControl = us;
            orders = _userControl.GetAllOrders(_userControl.GetActiveUser().Id);
            BestellingenDataGrid.ItemsSource = orders;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BestellingenDataGrid.ItemsSource = orders;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ObservableCollection<OrderInputDTO> orders2 = new();
            foreach (OrderInputDTO item in orders)
            {
                if (item.Id.ToString().Contains(txtId.Text))
                {
                    orders2.Add(item);
                }
            }
            BestellingenDataGrid.ItemsSource = orders2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow m = new();
            this.Close();
            m.Show();
        }
    }
}
