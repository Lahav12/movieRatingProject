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
        static MovieDB movieD = new MovieDB();
        Movie movie = movieD.SelectByID(3);

        public MoviePagexaml()
        {
            InitializeComponent();

            this.DescriptionText.Text = movie.Description;
            this.ScoreText.Text = movie.Rating.ToString();

            //poster
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(movie.Poster);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource imgSrc = biImg as ImageSource;
            this.PosterImage.Source = imgSrc;

           
        }


    }
}
