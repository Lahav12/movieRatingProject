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
        public int Likes { get; set; }
        public DateTime Date { get; set; }
        public Movie Movie { get; set; }
        
        public Person person { get; set; }
    }
}
