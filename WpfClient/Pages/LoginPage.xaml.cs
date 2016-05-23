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
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        /**
         * Login Method to handle Login Button
         * @param  object sender
         * @param  RoutedEventArgs e
         */
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;

            ApiOperations ops = new ApiOperations();
            User user = ops.AuthenticateUser(username, password);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }

            Globals.LoggedInUser = user;
            MessageBox.Show("Login successful");
            NavigationService.Navigate(new DetailsPage());
        }

        /**
         * Method to direct user to Register Page
         * @param object sender
         * @param RoutedEventArgs e
         */
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
