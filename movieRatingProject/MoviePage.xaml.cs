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
        private Movie _movie;

        public MoviePagexaml(Movie movie)
        {
            InitializeComponent();

            _movie = movie;

            if (!GlobalVariables.LogedIn)
            {
                CreateReviewGrid.Visibility = Visibility.Collapsed;
            }


            this.DescriptionText.Text = movie.Description;
            this.RatingText.Text = movie.Rating.ToString();
            this.MovieTitle.Content = movie.MovieName;
            this.Year.Text = movie.Year.ToString();
            this.Genre.Text = movie.Genre;

            this.PosterImage.Source = new BitmapImage(new Uri(movie.PosterPath, UriKind.RelativeOrAbsolute));

            CharacterDB characterDB = new CharacterDB();
            CharacterList characterList = characterDB.SelectByMovie(movie.ID);
            this.characterList.ItemsSource = characterList;

            ReviewDB reviewDB = new ReviewDB();
            ReviewList reviewList = reviewDB.SelectByMovieID(movie.ID);
            this.reviewList.ItemsSource = reviewList;
        }

        private void ScoreChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RatingText.Text, out int n))
            {
                RatingText.Text = ServiceApi.ScoreChange(Convert.ToInt32(this.RatingText.Text));
            }
        }

        private void SubmitReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDB userDB = new UserDB();
            User user = userDB.SelectByID(10);
          
            if (RatingComboBox.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Content.ToString(), out int rating))
            {
                ReviewDB reviewDB = new ReviewDB();
                int id = reviewDB.CreateReview(new Review
                {
                    user = GlobalVariables.user,
                    Text = ReviewText.Text,
                    Rating = rating,
                    movie = _movie,
                    Date = DateTime.Now.Date
                });
            }
        }
    }
}
