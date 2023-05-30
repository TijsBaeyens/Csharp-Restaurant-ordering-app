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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window {
        UserController _userControl;
        public RegisterWindow(UserController us) {
            _userControl = us;
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            this.Close();
            try {
                _userControl.CreateUser(txtUserLastName.Text, txtUserFirstName.Text, txtStreetName.Text, int.Parse(txtHouseNumber.Text), txtCity.Text, int.Parse(txtPostalCode.Text), txtCountry.Text, txtPhoneNumber.Text, txtEmail.Text, txtPass.Password, txtBusNumber.Text);
                _userControl.logInOrOut(txtUserLastName.Text, txtPass.Password, true);
                main.ActiveUser = _userControl.GetActiveUser();
                main.Visibility = Visibility.Visible;
                this.Close();
                main.Show();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoToLoginPage_Click(object sender, RoutedEventArgs e) {
            LoginWindow login = new LoginWindow(_userControl);
            login.Show();
            this.Close();
        }
    }
}
