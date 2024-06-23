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
using System.IO;

using Model;
using ViewModel;

namespace movieRatingProject
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void MovieList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminMovieList());
        }

        private void AddList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminAddFilm());
        }

        private void UserList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUserList());
        }
    }
}
