using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PersonList : List<Person>
    {
        public PersonList()
        { }
        public PersonList(IEnumerable<Person> lst) : base(lst)
        { }

        public PersonList(IEnumerable<BaseEntity> lst) :
            base(lst.Cast<Person>().ToList())
        { }
    }
}
