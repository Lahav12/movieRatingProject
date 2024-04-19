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
            //int h = personDB.CreatePerson(new Person { DateOfBirth = DateTime.Now, FirstName = "Brad", LastName = "Pit" });

            UserDB userDB = new UserDB();
            bool m = userDB.CreateUser(new User
            {
                FirstName = "Orit",
                LastName = "Lahav",
                DateOfBirth = DateTime.Now,
                Email = "Orlahav3107@Gmail.com",
                Password = "123456",
                UserName = "OrLahav",
                Phone = "0548114171"
            });

            //MovieDB movieDB = new MovieDB();
            //var result = movieDB.SelectAll();
            //Console.WriteLine(result[0].ID);

            //UserDB userDB = new UserDB();
            //UserList result = userDB.SelectAll();

            //Console.WriteLine(result[0].Password);
        }
    }
}
