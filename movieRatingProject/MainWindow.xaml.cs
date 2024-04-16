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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Movie_Click(null, null);
            
        }

        private void Movie_Click(object sender, RoutedEventArgs e)
        {
            MovieDB movieDB = new MovieDB();
            var result = movieDB.SelectAll();

            for (int i = 0; i < result.Count; i++)
            {
                lstView2.Items.Add(new Movie
                {
                    MovieName = result[i].MovieName,
                    Rating = result[i].Rating,
                    Duration = result[i].Duration,
                    Genre = result[i].Genre,
                    ReleaseDate = result[i].ReleaseDate
                });
            }
            
        }

        private void lstView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
