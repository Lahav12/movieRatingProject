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
    /// Interaction logic for FilmLibrary.xaml
    /// </summary>
    public partial class FilmLibrary : Page
    {
        public FilmLibrary()
        {
            InitializeComponent();

            MovieDB movieDB = new MovieDB();
            MovieList movies = movieDB.SelectAll();

            movieList.ItemsSource = movies;

            //List<ImdbMovie> topMovies = ServiceApi.GetTopMovies();

            //string topMovieString = $"Top 3 Movies At IMDB:\n1.{topMovies[0].Title}\n2.{topMovies[1].Title}\n3.{topMovies[2].Title}";
            //TopMovieBox.Text = topMovieString;
        }

        private void Movie_Click(object sender, SelectionChangedEventArgs e)
        {
            
            var item = (sender as ListView).SelectedItem as Movie;
            if (item != null)
            {
                NavigationService.Navigate(new MoviePagexaml(item));
            }
        }
    }
}
