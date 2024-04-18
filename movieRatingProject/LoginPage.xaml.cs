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
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDB userDB = new UserDB();
            UserList result = userDB.SelectAll();
            

            //UserNameText.Text = result[1].UserName;

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].UserName.Trim() == UserNameText.Text && result[i].Password.Trim() == PasswordText.Password)
                {
                    UserNameText.Text = "Loged In";

                    GlobalVariables.LogedIn = true;
                    GlobalVariables.logedUserName = result[i].UserName;
                    GlobalVariables.logedFirstName = result[i].FirstName;
                    GlobalVariables.logedId = result[i].ID;
                    GlobalVariables.isAdmin = result[i].IsAdmin;
                }
            }

            UserNameText.Text = GlobalVariables.isAdmin.ToString();

            if (!GlobalVariables.LogedIn)
                UserNameText.Text = "Not";
        }
    }
}
