using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Movie : BaseEntity
    {
        public string MovieName { get; set; }
        //public int MovieID { get; set; }
        public int Rating { get; set; }
        //public int Year { get; set; }
        //public Byte[] Poster { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
