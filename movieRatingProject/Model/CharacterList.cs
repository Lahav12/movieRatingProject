using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CharacterList : List<Character>
    {
        public CharacterList()
        { }
        public CharacterList(IEnumerable<Character> lst) : base(lst)
        { }

        public CharacterList(IEnumerable<BaseEntity> lst) :
            base(lst.Cast<Character>().ToList())
        { }
    }
}
