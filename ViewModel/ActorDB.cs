using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace ViewModel
{
    public class ActorDB : BaseDB
    {
        public ActorDB() : base("tblActors")
        {

        }

        public ActorList SelectAll()
        {
            _command.CommandText = "select tblPersons.* from tblActors inner join tblPersons on " +
                "tblPersons.personID = tblActors.personID ";
            ActorList lst = Select();
            return lst;
        }

        public Actor SelectByID(int id)
        {
            _command.CommandText = string.Format("select tblPersons.* from tblActors inner join tblPersons on " +
                "tblPersons.personID = tblActors.personID WHERE tblActors.personID = {0}",  id);
            ActorList lst = Select();
            return lst.FirstOrDefault();
        }

        public ActorList Select()
        {
            ActorList list = new ActorList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _reader = _command.ExecuteReader(); ;
                Actor actor;

                while (_reader.Read())
                {
                    actor = new Actor();

                    actor.ID = Convert.ToInt32(_reader["personID"]);
                    actor.FirstName = Convert.ToString(_reader["firstName"]);
                    actor.LastName = Convert.ToString(_reader["lastName"]);
                    actor.DateOfBirth = Convert.ToDateTime(_reader["dateOfBirth"]);

                    list.Add(actor);
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
