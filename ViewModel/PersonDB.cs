using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace ViewModel
{
    public class PersonDB : BaseDB
    {
        public PersonDB() : base("tblPersons")
        {

        }

        public PersonList SelectAll()
        {
            _command.CommandText = string.Format("select * from {0}", _tableName);
            PersonList lst = Select();
            return lst;
        }

        public Person SelectByID(int id)
        {
            _command.CommandText = string.Format("select * from tblPersons WHERE personID = {0}", id);
            PersonList lst = Select();
            return lst.FirstOrDefault();
        }

        public int CreatePerson(Person person)
        {
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("insert into tblPersons (firstName, lastName, dateOfBirth) " +
                    "values ('{0}', '{1}', '{2}') ;" +
                    " SELECT SCOPE_IDENTITY(); ", person.FirstName, person.LastName, person.DateOfBirth.Date.ToString("yyyy-MM-dd"));
                int id = (int)((decimal)_command.ExecuteScalar());
                return id;
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
            return -1;
        }

        private PersonList Select()
        {
            PersonList list = new PersonList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _reader = _command.ExecuteReader(); ;
                Person person;

                while (_reader.Read())
                {
                    person = new Person();

                    person.ID = Convert.ToInt32(_reader["personID"]);
                    person.FirstName = Convert.ToString(_reader["firstName"]);
                    person.LastName = Convert.ToString(_reader["lastName"]);
                    person.DateOfBirth = Convert.ToDateTime(_reader["dateOfBirth"]);

                    list.Add(person);
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

        public void RemovePerson(int id)
        {
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("DELETE FROM tblPersons WHERE personID = '{0}'", id);

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
