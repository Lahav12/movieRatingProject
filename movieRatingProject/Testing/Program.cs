using Model;
using ViewModel;
using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //ActorDB actorDB = new ActorDB();
            //var result = actorDB.SelectAll();
            //Console.WriteLine(result[1].ID);

            //PersonDB personDB = new PersonDB();
            //int h = personDB.CreatePerson(new Person { DateOfBirth = DateTime.Now, FirstName = "Ariel", LastName = "Efroni" });
            //int i = 1;

            MovieDB movieDB = new MovieDB();
            var result = movieDB.SelectAll();
            Console.WriteLine(result[0].ID);
        }
    }
}
