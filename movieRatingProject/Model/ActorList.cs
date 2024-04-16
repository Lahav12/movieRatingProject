using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ActorList : List<Actor>
    {
        public ActorList()
        { }
        public ActorList(IEnumerable<Actor> lst) : base(lst)
        { }

        public ActorList(IEnumerable<BaseEntity> lst) :
            base(lst.Cast<Actor>().ToList())
        { }
    }
}
