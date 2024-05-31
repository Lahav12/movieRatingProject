using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace ViewModel
{
    public class MovieDB : BaseDB
    {
        public MovieDB() : base("tblMovies")
        {

        }

        public MovieList SelectAll()
        {
            _command.CommandText = "SELECT * from " + _tableName;
            MovieList lst = Select();
            return lst;
        }

        public Movie SelectByID(int id)
        {
            _command.CommandText = string.Format("SELECT * from tblMovies WHERE movieID = {0}", id);
            MovieList lst = Select();
            return lst.FirstOrDefault();
        }

        public MovieList SelectByName(string movieName)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE movieName = {1}", _tableName, movieName);
            MovieList lst = Select();
            return lst;
        }

        public MovieList SelectByRating(int rating)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE rating = {1}", _tableName, rating);
            MovieList lst = Select();
            return lst;
        }

        public MovieList SelectByYear(int year)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE year = {1}", _tableName, year);
            MovieList lst = Select();
            return lst;
        }

        public MovieList SelectByGenre(string genre)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE genre = {1}", _tableName, genre);
            MovieList lst = Select();
            return lst;
        }

        public MovieList Select()
        {
            MovieList list = new MovieList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _reader = _command.ExecuteReader(); ;
                Movie movie;

                while (_reader.Read())
                {
                    movie = new Movie();

                    movie.ID = Convert.ToInt32(_reader["movieID"]);
                    movie.MovieName = Convert.ToString(_reader["movieName"]);
                    movie.Rating = Convert.ToInt32(_reader["rating"]);
                    //movie.Year = Convert.ToInt32(_reader["year"]);
                    movie.Duration = Convert.ToInt32(_reader["duration"]);
                    movie.Description = Convert.ToString(_reader["description"]);
                    movie.Genre = Convert.ToString(_reader["genre"]);
                    movie.ReleaseDate = Convert.ToDateTime(_reader["releaseDate"]);
                    movie.Poster = (byte[])_reader["poster"];
                    list.Add(movie);
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
