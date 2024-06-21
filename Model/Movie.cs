using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Model
{
    public class Movie : BaseEntity
    {
        public string MovieName { get; set; }
        public double Rating { get; set; }
        public string PosterPath { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        //public byte[] MovieImage { get; set; }

        //public BitmapImage BitmapImage
        //{
        //    get
        //    {
        //        if (MovieImage == null || MovieImage.Length == 0) return null;
        //        var image = new BitmapImage();
        //        using (var mem = new MemoryStream(MovieImage))
        //        {
        //            mem.Position = 0;
        //            image.BeginInit();
        //            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //            image.CacheOption = BitmapCacheOption.OnLoad;
        //            image.UriSource = null;
        //            image.StreamSource = mem;
        //            image.EndInit();
        //        }
        //        image.Freeze();
        //        return image;
        //    }
        //}
    }
}
