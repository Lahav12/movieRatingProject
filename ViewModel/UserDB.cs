﻿using System;
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

        public UserList SelectAll()
        {
            _command.CommandText = "select tblPersons.*, tblUsers.* from tblUsers inner join tblPersons on " +
                "tblPersons.personID = tblUsers.personID";
            UserList lst = Select();
            return lst;
        }

        public bool CreateUser(User user)
        {
            try
            {
                PersonDB pdb = new PersonDB();
                int pid = pdb.CreatePerson(user);

                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("insert into tblUsers (userName, password, email, phone, personID, isAdmin) " +
                    "values ('{0}', '{1}', '{2}', '{3}', {4}, 0);"
                   , user.UserName, user.Password, user.Email, user.Phone, pid);
                _command.ExecuteNonQuery();
                return true;

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

            return false;
        }

        public User SelectByID(int id)
        {
            _command.CommandText = string.Format("SELECT tblUsers.*, tblPersons.* FROM tblUsers " +
            "INNER JOIN tblPersons ON tblUsers.personID = tblPersons.personID " +
            "WHERE tblUsers.personID = {0}", id);
            UserList user = Select();
            return user.FirstOrDefault();
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

        public User SelectByUserName(string userName)
        {
            _command.CommandText = string.Format(" SELECT * from {0} WHERE userName = {1}", _tableName, userName);
            User lst = Select().FirstOrDefault();
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

                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    User user = new User();

                    // Reading values directly from _reader
                    user.ID = Convert.ToInt32(_reader["personID"]); // Assuming personID is retrieved from _reader
                    user.FirstName = _reader["firstName"].ToString();
                    user.LastName = _reader["lastName"].ToString();
                    user.DateOfBirth = Convert.ToDateTime(_reader["dateOfBirth"]);

                    // Reading values specific to User
                    user.Email = _reader["email"].ToString();
                    user.Password = _reader["password"].ToString();
                    user.UserName = _reader["userName"].ToString();
                    user.Phone = _reader["phone"].ToString();
                    user.IsAdmin = Convert.ToBoolean(_reader["isAdmin"]);

                    // Optionally, retrieve associated Person if needed
                    // PersonDB personDB = new PersonDB();
                    // user.Person = personDB.SelectByID(user.ID);

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

        public void RemoveUser(User user)
        {
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("DELETE FROM tblUsers WHERE personID = '{0}'", user.ID);
                PersonDB personDB = new PersonDB();
                personDB.RemovePerson(user.ID);

                _command.ExecuteNonQuery();
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
        }
    }
}
