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
    /// Interaction logic for AdminPopUp.xaml
    /// </summary>
    public partial class AdminPopUp : Window
    {
        UserController _userController;
        string Mode;
        Dish Dish;
        public AdminPopUp(UserController us, string mode)
        {
            InitializeComponent();
            _userController = us;
            Mode = mode;
        }
        public AdminPopUp(UserController us, Dish dish, string mode)
        {
            InitializeComponent();
            _userController = us;
            Dish = dish;
            Mode = mode;
        }

        private void Button_Click(object sender, RoutedEventArgs e){
            string name = txtDishName.Text;
            string description = txtDishDescription.Text;
            int price = Convert.ToInt32(txtPrice.Text);
            int amount = Convert.ToInt32(txtAmount.Text);
            if (Mode == "Edit")
            {
                _userController.UpdateDish(Dish.Id, name, description, price, amount);
            }
            else if (Mode == "Create")
            {
                _userController.CreateNewDish(name, description, price, amount);
            }
            AdministratorWindow w = new AdministratorWindow(_userController);
            w.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdministratorWindow w = new AdministratorWindow(_userController);
            w.Show();
            this.Close();
        }
    }
}
