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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public EventHandler LoginEvent;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDB userDB = new UserDB();
            UserList user = userDB.SelectAll();

            for (int i = 0; i < user.Count; i++)
            {
                if (user[i].UserName.Trim() == UserNameText.Text && user[i].Password.Trim() == PasswordText.Password)
                {
                    GlobalVariables.user = user[i];
                    GlobalVariables.LogedIn = true;
                    NavigationService.Navigate(new MainPage());
                }
            }

            if (LoginEvent != null)
            {
                LoginEvent(this, e);
            } 
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
