using System;
using Model;
using ViewModel;

namespace Testingg
{
    class Program
    {
        static void Main(string[] args)
        {
            ReviewDB reviewDB = new ReviewDB();

            int id = reviewDB.CreateReview(new Review
            {
                Rating = 6,
                user = new UserDB().SelectByID(41),
                Movie = new MovieDB().SelectByID(5),
                Text = "Super Duper",
                Date = DateTime.Now.Date
            }
            );
            Console.WriteLine(id);


        }
    }
}
