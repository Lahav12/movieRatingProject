using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Review :BaseEntity
    {
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public Movie movie { get; set; }
        public User user { get; set; }
    }
}
