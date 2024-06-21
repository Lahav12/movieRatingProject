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
        public string x;
        private byte[] imageBytes;

        public AdminPage()
        {
            InitializeComponent();
        }

        private void CreateFilmBtn_Click(object sender, RoutedEventArgs e)
        {
            MovieDB movieDB = new MovieDB();
            int id = movieDB.CreateMovie(new Movie
            {
                MovieName = this.FilmNameText.Text,
                Genre = this.FilmGenreText.Text,
                Duration = 90,
                Description = this.FilmDescriptionText.Text,
                Year = Convert.ToInt32(this.FilmYearText.Text),
                PosterPath = x
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = Directory.GetCurrentDirectory();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            
            if (op.ShowDialog() == true)
            {
                string appPath = @"..\..\Posters\";
                var fileNameToSave =  op.SafeFileName;
                //ImageUrl.Text = op.FileName;
                var imagePath = System.IO.Path.Combine(appPath + fileNameToSave);
                {
                    Directory.CreateDirectory(appPath);
                }
               // x = fileNameToSave; //Add Image path to user
                x = op.FileName;
                //File.Copy(op.FileName, imagePath,true);

              //  imageBytes = File.ReadAllBytes(op.FileName);

            }
        }
    }
}
