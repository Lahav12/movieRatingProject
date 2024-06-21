using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace ViewModel
{
    public class CharacterDB : BaseDB
    {
        public CharacterDB() : base("tblCharacters")
        {

        }

        public CharacterList SelectAll()
        {
            _command.CommandText = "select tblPersons.* from tblCharacters inner join tblPersons on " +
                "tblPersons.personID = tblCharacters.personID";
            CharacterList lst = Select();
            return lst;
        }

        public Character SelectByID(int id)
        {
            _command.CommandText = string.Format("select tblPersons.* from tblCharacters inner join tblPersons on" +
                "tblPersons.personID = tblCharacters.personID WHERE tblCharacters.personID = {0}", id);
            CharacterList lst = Select();
            return lst.FirstOrDefault();
        }

        public CharacterList SelectByMovie(int id)
        {
            _command.CommandText = string.Format("select tblCharacters.* from tblMovies inner join tblCharacters on " +
                "tblMovies.movieID = tblCharacters.movieID WHERE tblCharacters.movieID = {0} ", id);
            CharacterList lst = Select();
            return lst;
        }

        public CharacterList SelectByActor(int id)
        {
            _command.CommandText = string.Format("select tblPersons.* from tblCharacters inner join tblPersons on" +
                "tblPersons.personID = tblCharacters.actorID WHERE tblCharacters.actorID = {0}", id);
            CharacterList lst = Select();
            return lst;
        }

        public CharacterList Select()
        {
            CharacterList list = new CharacterList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _reader = _command.ExecuteReader(); ;
                Character character;

                while (_reader.Read())
                {
                    character = new Character();

                    character.ID = Convert.ToInt32(_reader["personID"]);

                    PersonDB personDB = new PersonDB();
                    Person person = personDB.SelectByID(character.ID);

                    character.FirstName = person.FirstName;
                    character.LastName = person.LastName;


                    if (_reader["movieID"] != DBNull.Value)
                    {
                        int movie = (int)_reader["movieID"];
                        MovieDB m = new MovieDB();
                        character.Movie = m.SelectByID(movie);
                    }

                    if (_reader["actorID"] != DBNull.Value)
                    {
                        int actor = (int)_reader["actorID"];
                        ActorDB a = new ActorDB();
                        character.Actor = a.SelectByID(actor);
                    }

                    list.Add(character);
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
