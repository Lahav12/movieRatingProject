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
        
        public bool SetRating(int movieID, double rating)
        {
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("update tblMovies set rating = {0} where movieID = {1}", rating, movieID);
                double id = (double)(_command.ExecuteNonQuery());
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
                    movie.Duration = Convert.ToInt32(_reader["duration"]);
                    movie.Description = Convert.ToString(_reader["description"]);
                    movie.Genre = Convert.ToString(_reader["genre"]);
                    movie.Year = Convert.ToInt32(_reader["year"]);
                    // movie.PosterPath = "Posters/" + (string)_reader["posterR"];
                    movie.PosterPath = (string)_reader["posterR"];
                    // movie.MovieImage = (byte[])_reader["Image"];
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

        public int CreateMovie(Movie movie)
        {
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("insert into tblMovies (movieName, rating, genre, duration, description, year, posterR) " +
                    "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}') ;" +
                    " SELECT SCOPE_IDENTITY(); ", movie.MovieName, 0, movie.Genre, movie.Duration, movie.Description, movie.Year, movie.PosterPath);
                //_command.Parameters.Add("@Image",movie.MovieImage);
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
    }
}
