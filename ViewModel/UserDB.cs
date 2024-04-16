using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

using Model;

namespace ViewModel
{
    public class UserDB : BaseDB
    {
        public UserDB() : base("tblUsers")
        {

        }

        public UserList SelectALL()
        {
            _command.CommandText = "SELECT * from" + _tableName;
            UserList lst = Select();
            return lst;
        }

        //public bool CreatePerson(User user)
        //{
        //    try
        //    {
                
        //        PersonDB pdb = new PersonDB();
        //        int pid = pdb.CreatePerson(user);

        //        _command.Connection = _connection;
        //        _connection.Open();

        //        _command.CommandText = string.Format("insert into {0} (firstName, lastName, dateOfBirth) " +
        //            "values ('{1}', '{2}', '{3}') ;"
        //           , _tableName, person.FirstName, person.LastName, person.DateOfBirth.Date.ToString("yyyy-MM-dd"));
        //        _command.ExecuteNonQuery());
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {

        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (_reader != null)
        //            _reader.Close();
        //        if (_connection.State == System.Data.ConnectionState.Open)
        //            _connection.Close();
        //    }

        //    return false;
        //}

        public UserList SelectByID(int id)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE personID = {1}", _tableName, id);
            UserList lst = Select();
            return lst;
        }

        public UserList SelectByFirstName(string firstName)
        {
            _command.CommandText = string.Format("select tblPersons.* from tblUsers inner join tblPersons on " +
                    "tblPersons.firstName = {0}", firstName);
            UserList lst = Select();
            return lst;
        }

        public UserList SelectByLastName(string lastName)
        {
            _command.CommandText = string.Format("select tblPersons.* from tblUsers inner join tblPersons on " +
                    "tblPersons.lastName = {0}", lastName);
            UserList lst = Select();
            return lst;
        }

        public User SelectByFullName(string firstName, string lastName)
        {
            _command.CommandText = string.Format("select tblPersons.* from tblUsers inner join tblPersons on " +
                    "tblPersons.firstName = {0} and tblPersons.lastName = {1}", firstName, lastName);
            UserList lst = Select();
            return lst.FirstOrDefault();
        }

        public UserList SelectByUserName(string userName)
        {
            _command.CommandText = string.Format(" SELECT * from {0} WHERE userName = {1}", _tableName, userName);
            UserList lst = Select();
            return lst;
        }

        public UserList SelectByPassword(string password)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE password = {1}", _tableName, password);
            UserList lst = Select();
            return lst;
        }

        public UserList SelectByIsAdmin()
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE isAdmin = 1", _tableName);
            UserList lst = Select();
            return lst;
        }



        public UserList Select()
        {
            UserList list = new UserList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _reader = _command.ExecuteReader(); ;
                User user;

                while (_reader.Read())
                {
                    user = new User();



                    //tblPerson
                    user.FirstName = _reader["firstName"].ToString();
                    user.LastName = _reader["lastName"].ToString();
                    user.DateOfBirth = Convert.ToDateTime(_reader["dateOfBirth"]);
                    user.ID = Convert.ToInt32(_reader["personID"]);
                    //מה עושים עם תמונה?

                    //tblUser
                    user.Email = _reader["email"].ToString();
                    user.Password = _reader["password"].ToString();
                    user.UserName = _reader["userName"].ToString();
                    user.Phone = _reader["phone"].ToString();
                    user.IsAdmin = Convert.ToBoolean(_reader["isAdmin"]);

                    list.Add(user);
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (_reader != null)
                    _reader.Close();
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
            return list;
        }
    }
}
