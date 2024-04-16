using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReviewList : List<Review>
    {
        public ReviewList()
        { }
        public ReviewList(IEnumerable<Review> lst) : base(lst)
        { }

        public ReviewList(IEnumerable<BaseEntity> lst) :
            base(lst.Cast<Review>().ToList())
        { }
    }
}
