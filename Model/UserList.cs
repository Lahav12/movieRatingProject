using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserList : List<User>
    {
        public UserList()
        { }
        public UserList(IEnumerable<User> lst) : base(lst)
        { }

        public UserList(IEnumerable<BaseEntity> lst) :
            base(lst.Cast<User>().ToList())
        { }
    }
}
