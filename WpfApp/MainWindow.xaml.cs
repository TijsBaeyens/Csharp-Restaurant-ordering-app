using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WebApplication1.Controllers;
using WebApplication1.Models.DTO.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private UserController _userControl;
        private ObservableCollection<DishInputDTO> dishes;
        private ObservableCollection<OrderDishInputDTO> orderDishInputDTOs;
        private string _conn;
        private User _activeUser { get; set;}
        public User ActiveUser { get; internal set; }
        public int Amount = 0;

        public MainWindow() {
            InitializeComponent();
            _conn = System.Configuration.ConfigurationManager.ConnectionStrings["ADOconnSQL"].ConnectionString;
            _userControl = new UserController(_conn);
            if (!_userControl.Heartbeat())
            {
                MessageBox.Show("Database connection failed");
                Application.Current.Shutdown();
            }
            else
            {
                dishes = _userControl.GetAllAvailableDishes();
                test.ItemsSource = _userControl.GetAllAvailableDishes();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                _activeUser = _userControl.GetActiveUser();
                test2.ItemsSource = _userControl.GetSpecificOrderInDTO(_activeUser.Id);
            } catch {
                LoginWindow loginWindow = new LoginWindow(_userControl);
                this.Close();
                loginWindow.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            _userControl.logInOrOut(_activeUser.LastName, _activeUser.Password, false);
            LoginWindow login = new LoginWindow(_userControl);
            this.Close();
            login.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            try {
                int orderId = 0;
                int dishId = 0;
                int amount = 0;
                try {
                    orderId = _userControl.GetActiveOrder(_activeUser.Id).Id;
                } catch {
                    _userControl.CreateOrder(_activeUser.Id);
                    orderId = _userControl.GetActiveOrder(_activeUser.Id).Id;
                }
                DishInputDTO dishInput = (DishInputDTO)test.SelectedItem;
                if (dishInput == null)
                {
                    MessageBox.Show("Selecteer een gerecht om toe te voegen");
                } else if (numberTextBox.Text == "0")
                {
                    MessageBox.Show("Selecteer een aantal om toe te voegen");
                }
                else
                {
                    dishId = _userControl.GetDishByName(dishInput.Name).Id;
                    amount = Amount;
                    _userControl.AddOrderDish(orderId, dishId, amount);
                    numberTextBox.Text = "0";
                    Amount = 0;
                    test.ItemsSource = _userControl.GetAllAvailableDishes();
                    test2.ItemsSource = _userControl.GetSpecificOrderInDTO(_activeUser.Id);
                }
            } catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }
        private void PlusButton_Click(object sender, RoutedEventArgs e) {
            Amount++;
            numberTextBox.Text = Amount.ToString();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e) {
            Amount--;
            if (Amount < 0)
            {
                Amount = 0;
            }
            numberTextBox.Text = Amount.ToString();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) {
            try {
                int orderId = 0;
                int dishId = 0;
                int amount = 0;
                try {
                    orderId = _userControl.GetActiveOrder(_activeUser.Id).Id;
                } catch {
                    throw new Exception("You have no active order");
                }
                OrderDishInputDTO dishInput = (OrderDishInputDTO)test2.SelectedItem;
                if (dishInput == null)
                {
                    MessageBox.Show("Selecteer een gerecht om te verwijderen");
                }
                else if (numberTextBox.Text == "0")
                {
                    MessageBox.Show("Selecteer een aantal om te verwijderen");
                }
                else
                {
                    dishId = _userControl.GetDishByName(dishInput.DishName).Id;
                    amount = dishInput.Quantity - Amount;
                    _userControl.ChangeOrderDishAmount(orderId, dishId, amount);
                    numberTextBox.Text = "0";
                    Amount = 0;
                    test.ItemsSource = _userControl.GetAllAvailableDishes();
                    test2.ItemsSource = _userControl.GetSpecificOrderInDTO(_activeUser.Id);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) {
            try {
                foreach (OrderDishInputDTO item in test2.Items) {
                    _userControl.DeleteAllOrderDish(_userControl.GetActiveOrder(_activeUser.Id).Id);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            test2.ItemsSource = _userControl.GetSpecificOrderInDTO(_activeUser.Id);
            test.ItemsSource = _userControl.GetAllAvailableDishes();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e) {
            BetaalWindow w = new BetaalWindow(_userControl);
            w.Show();
            this.Close();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AlleBestellingenWindow window = new AlleBestellingenWindow(_userControl);
            window.Show();
            this.Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            AdministratorWindow w = new AdministratorWindow(_userControl);
            w.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InstellingenWindow instellingenWindow = new InstellingenWindow(_activeUser, _userControl);
            instellingenWindow.Show();
            this.Close();
        }
    }
}
