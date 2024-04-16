using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Character : Person
    {
        public Movie Movie { get; set; }
        
        public Actor Actor{ get; set; }
    }
}
