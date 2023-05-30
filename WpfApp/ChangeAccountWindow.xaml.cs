using DomainLayer.Models;
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
using WebApplication1.Controllers;

namespace WpfApp {
    /// <summary>
    /// Interaction logic for ChangeAccountWindow.xaml
    /// </summary>
    public partial class ChangeAccountWindow : Window {
        public UserController _userControl;
        public ChangeAccountWindow(UserController us) {
            InitializeComponent();
            _userControl = us;
            txtUserLastName.Text = _userControl.GetActiveUser().LastName;
            txtUserFirstName.Text = _userControl.GetActiveUser().FirstName;
            txtStreetName.Text = _userControl.GetActiveUser().Street;
            txtHouseNumber.Text = _userControl.GetActiveUser().HouseNumber.ToString();
            txtCity.Text = _userControl.GetActiveUser().City.ToString();
            txtPostalCode.Text = _userControl.GetActiveUser().PostalCode.ToString();
            txtPhoneNumber.Text = _userControl.GetActiveUser().PhoneNumber.ToString();
            txtEmail.Text = _userControl.GetActiveUser().EmailAddress.ToString();
            txtBusNumber.Text = _userControl.GetActiveUser().BusNumber.ToString();
            txtCountry.Text = _userControl.GetActiveUser().Country.ToString();
        }

        private void btnChangeAccount_Click(object sender, RoutedEventArgs e) {
            if (int.TryParse(txtHouseNumber.Text, out int houseNumber) == false) {
                MessageBox.Show("House number must be a number");
                return;
            } else if (int.TryParse(txtPostalCode.Text, out int postalCode) == false) {
                MessageBox.Show("Postal code must be a number");
                return;
            } else if (txtPass.Password == "") {
                MessageBox.Show("Password cannot be empty");
                return;
            } else if (txtPass2.Password != txtPass.Password) {
                MessageBox.Show("Passwords do not match");
                return;
            } else {
                _userControl.UpdateUser(_userControl.GetActiveUser().Id, txtUserLastName.Text, txtUserFirstName.Text, txtStreetName.Text, int.Parse(txtHouseNumber.Text), txtCity.Text, int.Parse(txtPostalCode.Text), txtCountry.Text, txtPhoneNumber.Text, txtEmail.Text, txtPass.Password, txtBusNumber.Text, true);

            }
            MainWindow main = new MainWindow();
            main.ActiveUser = _userControl.GetActiveUser();
            main.Show();
            this.Close();
        }
    }
}
