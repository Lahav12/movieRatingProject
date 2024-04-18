using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieRatingProject
{
    public static class GlobalVariables
    {
        public static bool LogedIn { get; set; }

        public static string logedUserName { get; set; }
        public static string logedFirstName { get; set; }
        public static int logedId { get; set; }
        public static bool isAdmin { get; set; }
    }
}
