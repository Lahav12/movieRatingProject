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
using Microsoft.Win32;
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

        //private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        //{ 
        //    UserDB userDB = new UserDB();
        //    if (FormValidation())
        //    {
        //        bool registered = userDB.CreateUser(new User
        //        {
        //            FirstName = FirstNameText.Text,
        //            LastName = LastNameText.Text,
        //            UserName = UserNameText.Text,
        //            Password = PasswordText.Text,
        //            Email = EmailText.Text,
        //            Phone = PhoneText.Text,
        //            DateOfBirth = DateText.SelectedDate.Value
        //        });
        //    }

        //    //if (registered)
        //    //    FirstNameText.Text = "Registered";
        //    //else
        //    //    FirstNameText.Text = "Not";
        //}

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = string.Empty; // Clear previous error messages

            if (FormValidation())
            {
                UserDB userDB = new UserDB();
                bool registered = userDB.CreateUser(new User
                {
                    FirstName = FirstNameText.Text,
                    LastName = LastNameText.Text,
                    UserName = UserNameText.Text,
                    Password = PasswordText.Text,
                    Email = EmailText.Text,
                    Phone = PhoneText.Text,
                    DateOfBirth = DateText.SelectedDate.Value
                });

                if (registered)
                    MessageBox.Show("Registered successfully!");
                else
                    ErrorTextBlock.Text = "Registration failed.";
            }
        }

        private bool FormValidation()
        {
            bool valid = true;

            // Length Check
            if (OverTen(FirstNameText.Text))
            {
                ErrorTextBlock.Text += "First Name must be under 10 characters.\n";
                valid = false;
            }
            if (OverTen(LastNameText.Text))
            {
                ErrorTextBlock.Text += "Last Name must be under 10 characters.\n";
                valid = false;
            }
            if (OverTen(UserNameText.Text))
            {
                ErrorTextBlock.Text += "User Name must be under 10 characters.\n";
                valid = false;
            }
            if (OverTen(PasswordText.Text))
            {
                ErrorTextBlock.Text += "Password must be under 10 characters.\n";
                valid = false;
            }

            // Password Confirmation
            if (PasswordText.Text != ConfirmPasswordText.Text)
            {
                ErrorTextBlock.Text += "Passwords must be identical.\n";
                valid = false;
            }

            // Not null check
            valid &= NotNull(FirstNameText, "First Name");
            valid &= NotNull(LastNameText, "Last Name");
            valid &= NotNull(UserNameText, "User Name");
            valid &= NotNull(PasswordText, "Password");
            valid &= NotNull(ConfirmPasswordText, "Confirm Password");

            // Date of birth check
            if (DateText.SelectedDate == null)
            {
                ErrorTextBlock.Text += "You must select a date of birth.\n";
                valid = false;
            }

            return valid;
        }

        private bool NotNull(TextBox value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value.Text))
            {
                ErrorTextBlock.Text += $"{fieldName} cannot be empty.\n";
                return false;
            }
            return true;
        }

        private bool NotNull(PasswordBox value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value.Password))
            {
                ErrorTextBlock.Text += $"{fieldName} cannot be empty.\n";
                return false;
            }
            return true;
        }

        private bool OverTen(string str)
        {
            return str.Length > 10;
        }

        private void uploadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Image Files|" +
                "*.jpg;*.gif;*jpeg;*bmp;";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.InitialDirectory = @"C:\Users\orlah\source\repos\movieRatingProject\movieRatingProject\Assets\";

            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                //switch (saveFileDialog1.FilterIndex)
                //{
                //    case 1:
                //        this.button2.Image.Save(fs,
                //          System.Drawing.Imaging.ImageFormat.Jpeg);
                //        break;

                //    case 2:
                //        this.button2.Image.Save(fs,
                //          System.Drawing.Imaging.ImageFormat.Bmp);
                //        break;

                //    case 3:
                //        this.button2.Image.Save(fs,
                //          System.Drawing.Imaging.ImageFormat.Gif);
                //        break;
                //}

                fs.Close();
            }


        }
    }

    
}
