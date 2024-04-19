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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        { 
            UserDB userDB = new UserDB();
            if (FormValidation())
            {
                bool registered = userDB.CreateUser(new User
                {
                    FirstName = FirstNameText.Text,
                    LastName = LastNameText.Text,
                    UserName = UserNameText.Text,
                    Password = PasswordText.Text,
                    Email = EmailText.Text,
                    Phone = ""
                });
            }

            //if (registered)
            //    FirstNameText.Text = "Registered";
            //else
            //    FirstNameText.Text = "Not";
        }

        private bool FormValidation()
        {
            bool valid = true;

            //Under 10 Check
            if (OverTen(FirstNameText.Text))
            {
                FirstNameText.Text = "First Name Must Be Under 10 Characters";
                valid = false;
            }
            if (OverTen(LastNameText.Text))
            {
                LastNameText.Text = "Last Name Must Be Under 10 Characters";
                valid = false;
            }
            if (OverTen(UserNameText.Text))
            { 
                UserNameText.Text = "User Name Must Be Under 10 Characters";
                valid = false;
            }
            if (OverTen(PasswordText.Text))
            {
                PasswordText.Text = "Password Must Be Under 10 Characters";
                valid = false;
            }
            //if (IsUnderTen(PhoneText.Text))
            //    PhoneText.Text = "First Name Must Be Under 10 Characters";
            //    valid = false;

            //Password Confirmation
            if (PasswordText.Text != ConfirmPasswordText.Text)
            {
                ConfirmPasswordText.Text = "Password Must Be Identical";
                valid = false;
            }


            return valid;
        }

        private bool OverTen(string str)
        {
            if (str.Length > 10)
                return true;
            return false;
        }
    }
}
