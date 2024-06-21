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
    public class ReviewDB : BaseDB
    {
        public ReviewDB() : base("tblMovies")
        {

        }

        public ReviewList SelectAll()
        {
            _command.CommandText = "SELECT * from " + _tableName;
            ReviewList lst = Select();
            return lst;
        }

        public Review SelectByID(int id)
        {
            _command.CommandText = string.Format("SELECT * from tblReviews WHERE reviewID = {0}", id);
            ReviewList lst = Select();
            return lst.FirstOrDefault();
        }

        public ReviewList SelectByMovieID(int movieID)
        {
            _command.CommandText = string.Format("SELECT * from tblReviews WHERE movieID = {0}", movieID);
            ReviewList lst = Select();
            return lst;
        }

        public int SelectRatingByID(int movieID, int personID)
        {
            _command.CommandText = string.Format("SELECT rating from {0} where movieID = {1} and personID = {2}", _tableName, movieID, personID);
            ReviewList review = Select();
            int rating = review.FirstOrDefault().Rating;
            return rating;
        }

        public double GetMovieRatings(int movieId)
        {
            try
            {
                _command.CommandText = "SELECT AVG(rating) FROM tblReviews WHERE movieID = @movieID";
                _command.Parameters.Clear(); // Clear existing parameters
                _command.Parameters.AddWithValue("@movieID", movieId);

                _command.Connection = _connection;

                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                object result = _command.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    double averageRating = Convert.ToDouble(result);
                    System.Diagnostics.Debug.WriteLine($"Average Rating for movieID {movieId}: {averageRating}");
                    return averageRating;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"No reviews found for movieID {movieId}");
                    return 0; // Return 0 if there are no reviews
                }
            }
            catch (InvalidCastException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid cast error in GetMovieRatings: {ex.Message}");
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL error in GetMovieRatings: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General error in GetMovieRatings: {ex.Message}");
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return -1; // Return -1 to indicate an error
        }

        public ReviewList Select()
        {
            ReviewList list = new ReviewList();
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                using (_reader = _command.ExecuteReader())
                {
                    while (_reader.Read())
                    {
                        Review review = new Review
                        {
                            ID = Convert.ToInt32(_reader["reviewID"]),
                            Rating = Convert.ToInt32(_reader["rating"]),
                            Date = Convert.ToDateTime(_reader["date"]).Date,
                            Text = _reader["text"].ToString()
                        };

                        if (_reader["movieID"] != DBNull.Value)
                        {
                            int movieId = (int)_reader["movieID"];
                            review.movie = new MovieDB().SelectByID(movieId);
                        }

                        if (_reader["personID"] != DBNull.Value)
                        {
                            int userId = (int)_reader["personID"];
                            review.user = new UserDB().SelectByID(userId);
                        }

                        list.Add(review);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Select: {ex.Message}");
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

        //public ReviewList Select()
        //{
        //    ReviewList list = new ReviewList();
        //    try
        //    {
        //        _command.Connection = _connection;
        //        _connection.Open();

        //        _reader = _command.ExecuteReader(); ;
        //        Review review;

        //        while (_reader.Read())
        //        {
        //            review = new Review();

        //            //int personID = Convert.ToInt32(_reader["personID"]);
        //            //review.user = new UserDB().SelectByID(personID);
        //            ////TODO get person from person

        //            review.ID = Convert.ToInt32(_reader["reviewID"]);
        //            review.Rating = Convert.ToInt32(_reader["rating"]);
        //            review.Date = Convert.ToDateTime(_reader["date"]);
        //            review.Text = _reader["text"].ToString();

        //            if (_reader["movieID"] != DBNull.Value)
        //            {
        //                int movie = (int)_reader["movieID"];
        //                MovieDB m = new MovieDB();
        //                review.movie = m.SelectByID(movie);
        //            }

        //            if (_reader["personID"] != DBNull.Value)
        //            {
        //                int user = (int)_reader["personID"];
        //                UserDB u = new UserDB();
        //                review.user = u.SelectByID(user);
        //            }

        //            list.Add(review);
        //        }
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
        //    return list;
        //}

        public int CreateReview(Review review)
        {
            try
            {
                _command.Connection = _connection;
                _connection.Open();

                _command.CommandText = string.Format("insert into tblReviews(personID, text, rating, movieID, date) " +
                    "values({0}, '{1}', {2}, '{3}', '{4}') ; " +
                "SELECT SCOPE_IDENTITY(); ", review.user.ID, review.Text, review.Rating, review.movie.ID, DateTime.Now.ToString("yyyy-MM-dd"));

                int id = (int)((decimal)_command.ExecuteScalar());
                
                MovieDB movieDB = new MovieDB();

                double updatedRating = GetMovieRatings(review.movie.ID);
                movieDB.SetRating(review.movie.ID, updatedRating);
                
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
