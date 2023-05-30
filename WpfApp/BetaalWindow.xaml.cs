using DomainLayer.Models;
using PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for BetaalWindow.xaml
    /// </summary>
    public partial class BetaalWindow : Window
    {
        UserController _userControl;
        public BetaalWindow(UserController us)
        {
            InitializeComponent();
            _userControl = us;
            decimal totaal = 0;
            foreach (OrderDish item in _userControl.GetSpecificOrder(_userControl.GetActiveUser().Id))
            {
                totaal += item.Amount * _userControl.GetDishById(item.DishId).Price;
            }
            txtTotaal.Text = "€ " + totaal.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rateLimiter = new RateLimiter(1, TimeSpan.FromSeconds(10));
                if (!rateLimiter.CanMakeRequest())
                {
                    MessageBox.Show("Probeer het later opnieuw");
                    return;
                } else {
                if (_userControl.GetSpecificOrder(_userControl.GetActiveUser().Id).Count > 0 && _userControl.GetActiveUser() != null)
                {
                    _userControl.PayOrder(_userControl.GetActiveUser().Id);
                    MainWindow m = new();
                    Mail();
                    this.Close();
                    m.Show();
                }
                else
                {
                    MessageBox.Show("U heeft nog geen bestelling geplaatst");
                }
            }
                }
            catch
            {
                MessageBox.Show("U heeft nog geen bestelling geplaatst");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow w = new MainWindow();
                w.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan");
            }
        }
        private void Mail()
        {
            try
            {
                string fromMail = "web3754@gmail.com";
                string fromPassword = "nwxfhdgticffgxja";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromMail);
                mail.Subject = "Order";
                mail.To.Add(_userControl.GetActiveUser().EmailAddress);
                mail.Body = "Uw bestelling is geplaatst";
                mail.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(mail);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
