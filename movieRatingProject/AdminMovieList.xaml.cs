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
    /// Interaction logic for AdminMovieList.xaml
    /// </summary>
    public partial class AdminMovieList : Page
    {
        public AdminMovieList()
        {
            InitializeComponent();

            MovieDB movieDB = new MovieDB();
            MovieList movies = movieDB.SelectAll();

            MovieListGrid.ItemsSource = movies;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MovieDB movieDB = new MovieDB();

            for (int i = 0; i < MovieListGrid.SelectedItems.Count; i++)
            {
                if (MovieListGrid.SelectedItems[i] is Movie selectedMovie)
                {
                    movieDB.RemoveMovie(selectedMovie);
                }
            }
        }

        private void AddCharacterBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
