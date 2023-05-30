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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for InstellingenWindow.xaml
    /// </summary>
    public partial class InstellingenWindow : Window
    {
        User _activeUser;
        UserController _userControl;
        public InstellingenWindow(User activeUser, UserController userControl)
        {
            InitializeComponent();
            _activeUser = activeUser;
            _userControl = userControl;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            User active = _activeUser;
            _userControl.logInOrOut(_activeUser.LastName, active.Password, false);
            _userControl.DeleteUser(active.Id);
            _activeUser = null;
            LoginWindow login = new LoginWindow(_userControl);
            this.Close();
            login.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeAccountWindow w = new ChangeAccountWindow(_userControl);
            w.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _userControl.logInOrOut(_activeUser.LastName, _activeUser.Password, false);
            LoginWindow login = new LoginWindow(_userControl);
            this.Close();
            login.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
