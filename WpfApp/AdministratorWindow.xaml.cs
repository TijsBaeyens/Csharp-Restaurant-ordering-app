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
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        UserController _userController;
        public AdministratorWindow(UserController us)
        {
            InitializeComponent();
            _userController = us;
            DishesDataGrid.ItemsSource = _userController.GetAllDishes();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminPopUp w = new AdminPopUp(_userController, "Create");
            w.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dish dish = (Dish)DishesDataGrid.SelectedItem;
            if (dish == null)
            {
                MessageBox.Show("Selecteer een gerecht");
            }
            else
            {
                AdminPopUp w = new AdminPopUp(_userController, dish, "Edit");
                w.Show();
                this.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Dish dish = (Dish)DishesDataGrid.SelectedItem;
            if (dish == null)
            {
                MessageBox.Show("Selecteer een gerecht");
            }
            else
            {
                _userController.DeleteDish(dish.Id);
                DishesDataGrid.ItemsSource = _userController.GetAllDishes();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
