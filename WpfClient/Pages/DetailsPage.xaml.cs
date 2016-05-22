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
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        private void detailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            FetchUserDetails();
            ShowUserInfo();
        }

        private void FetchUserDetails()
        {
            ApiOperations ops = new ApiOperations();
            User user = ops.GetUserDetails(Globals.LoggedInUser);
            if (user == null)
            {
                MessageBox.Show("Session expired");
                // Navigate back to login page
                NavigationService.Navigate(new LoginPage());
            }

            Globals.LoggedInUser = user;
        }

        private void ShowUserInfo()
        {
            tbkWelcome.Text += " " + Globals.LoggedInUser.Username;
            tbkFname.Text = Globals.LoggedInUser.Firstname;
            tbkMname.Text = Globals.LoggedInUser.Middlename;
            tbkLname.Text = Globals.LoggedInUser.Lastname;
            tbkAge.Text = Globals.LoggedInUser.Age.ToString();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Globals.LoggedInUser = null;
            NavigationService.Navigate(new LoginPage());
        }
    }
}
