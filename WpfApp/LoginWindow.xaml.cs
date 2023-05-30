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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {

        private UserController _userControl;
        public LoginWindow(UserController us) {
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

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            try {
            MainWindow main = new MainWindow();
            _userControl.logInOrOut(txtUser.Text, txtPass.Password, true);
            main.ActiveUser = _userControl.GetActiveUser();
            main.Visibility = Visibility.Visible;
            this.Close();
            main.Show();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoToRegister_Click(object sender, RoutedEventArgs e) {
            RegisterWindow register = new RegisterWindow(_userControl);
            this.Close();
            register.Show();
        }
    }
}
