using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MovieList :List<Movie>
    {
        public MovieList()
        { }
        public MovieList(IEnumerable<Movie> lst) : base(lst)
        { }

        public MovieList(IEnumerable<BaseEntity> lst) :
            base (lst.Cast<Movie>().ToList())
        { }

    }
}
