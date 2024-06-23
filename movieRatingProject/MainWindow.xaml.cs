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
using Model;
using ViewModel;

namespace movieRatingProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden; // Hide the Frame Navigation
            UpdateMenu();
        }

        private void HomeItem_Selected(object sender, RoutedEventArgs e)
        {
            this.myFrame.Navigate(new FilmLibrary());
            
        }

        private void AdminPage_Selected(object sender, RoutedEventArgs e)
        {
            if (GlobalVariables.user != null && GlobalVariables.user.IsAdmin)
            {
                this.myFrame.Navigate(new AdminPage());
            }
            else
            {
                MessageBox.Show("Access Denied. Admins only.");
            }
        }

        private void Profile_Selected(object sender, RoutedEventArgs e)
        {
            var page = new LoginPage();
            page.LoginEvent += new EventHandler(LoginPage_LoggedIn);
            this.myFrame.Navigate(page);
        }

        private void LoginPage_LoggedIn(object sender, EventArgs e)
        {
            UpdateMenu();
        }

        private void Logout_Selected(object sender, RoutedEventArgs e)
        {
            GlobalVariables.user = null;
            GlobalVariables.LogedIn = false;
            UpdateMenu();
            this.myFrame.Navigate(new LoginPage()); // Navigate to a home or login page after logout
        }

        private void UpdateMenu()
        {
            if (GlobalVariables.user != null)
            {
                LoginBtn.Visibility = Visibility.Collapsed;
                
                if (GlobalVariables.user.IsAdmin)
                {
                    AdminBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    AdminBtn.Visibility = Visibility.Collapsed;
                }
                LogOut.Visibility = Visibility.Visible;
            }
            else
            {
                LoginBtn.Visibility = Visibility.Visible;
                AdminBtn.Visibility = Visibility.Collapsed;
                LogOut.Visibility = Visibility.Collapsed;
            }
        }
    }
}
