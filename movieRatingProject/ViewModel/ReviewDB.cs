using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace ViewModel
{
    class ReviewDB : BaseDB
    {
        public ReviewDB() : base("tblReviews")
        {

        }

        public ReviewList SelectAll()
        {
            _command.CommandText = "SELECT * from " + _tableName;
            ReviewList lst = Select();
            return lst;
        }

        public ReviewList SelectByID(int id)
        {
            _command.CommandText = string.Format("SELECT * from tblReviews WHERE reviewID = {0}", id);
            ReviewList lst = Select();
            return lst;
        }

        public ReviewList SelectByName(string movieName)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE movieName = {1}", _tableName, movieName);
            ReviewList lst = Select();
            return lst;
        }

        public ReviewList SelectByRating(int rating)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE rating = {1}", _tableName, rating);
            ReviewList lst = Select();
            return lst;
        }

        public ReviewList SelectByYear(int year)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE year = {1}", _tableName, year);
            ReviewList lst = Select();
            return lst;
        }

        public ReviewList SelectByGenre(string genre)
        {
            _command.CommandText = string.Format("SELECT * from {0} WHERE genre = {1}", _tableName, genre);
            ReviewList lst = Select();
            return lst;
        }

        public ReviewList Select()
        {
            ReviewList list = new ReviewList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _reader = _command.ExecuteReader(); ;
                Review review;

                while (_reader.Read())
                {
                    review = new Review();

                    

                    int personID = Convert.ToInt32(_reader["personID"]);
                    //TODO get person from person

                    review.ID = Convert.ToInt32(_reader["reviewID"]);
                    review.Likes = Convert.ToInt32(_reader["likes"]);
                    review.Date = Convert.ToDateTime(_reader["date"]);
                    review.Text = _reader["text"].ToString();

                    if (_reader["MovieID"] != DBNull.Value)
                    {
                        int movie = (int)_reader["movieID"];
                        MovieDB m = new MovieDB();
                        review.Movie = m.SelectByID(movie);
                    }
                    

                    list.Add(review);
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
