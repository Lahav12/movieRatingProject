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
using System.IO;

namespace movieRatingProject
{
    /// <summary>
    /// Interaction logic for MoviePagexaml
    /// </summary>
    public partial class MoviePagexaml : Page
    {
        

        public MoviePagexaml(Movie movie)
        {
            InitializeComponent();

            // MovieDB movieD = new MovieDB();
            //Movie movie = movieD.SelectByID(id);

            this.DescriptionText.Text = movie.Description;
            this.ScoreText.Text = movie.Rating.ToString();
            this.MovieTitle.Content = movie.MovieName;
            this.Year.Text = movie.Year.ToString();
            this.Genre.Text = movie.Genre;

            

            //this.PosterImage.Source = new ImageSource(movie.PosterPath);
            this.PosterImage.Source = new BitmapImage(new Uri(movie.PosterPath, UriKind.Relative)); //is relative

        }

        private void ScoreChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ScoreText.Text, out int n))
            {
                ScoreText.Text = ServiceApi.ScoreChange(Convert.ToInt32(this.ScoreText.Text));
            }
        }
    }
}
