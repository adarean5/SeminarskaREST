using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace SeminarskaREST
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Movies" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Movies.svc or Movies.svc.cs at the Solution Explorer and start debugging.
    public class Movies : IMovies
    {
        string csMovie = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;

        public List<Movie> GetMovieList()
        {
            var retVal = new List<Movie>();

            using (SqlConnection con = new SqlConnection(csMovie))
            {
                con.Open();
                string sql = "SELECT * FROM MOVIES";
                SqlCommand cmd = new SqlCommand(sql, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retVal.Add(new Movie
                        {
                            id = Convert.ToInt32(reader[0]),
                            movieTitle = reader.GetString(1),
                            movieDesc = reader.GetString(2),
                            movieDate = reader.GetString(3),
                            movieRating = reader.GetDouble(4)//Convert.ToInt32(reader[4])
                    });
                    }
                }
                con.Close();
                return retVal;
            }
        }

        public Movie GetMovie(string id)
        {
            Movie movie = new Movie();

            using (SqlConnection con = new SqlConnection(csMovie))
            {
                con.Open();
                string sql = "SELECT * FROM Movies WHERE MovieID=@param1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("param1", id));

                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        movie.id = Convert.ToInt32(reader[0]);
                        movie.movieTitle = reader.GetString(1);
                        movie.movieDesc = reader.GetString(2);
                        movie.movieDate = reader.GetString(3);
                        movie.movieRating = reader.GetDouble(4);//Convert.ToInt32(reader[4]);
                    }
                }
                con.Close();
            }
            return movie;
        }

        public void AddMovie(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(csMovie))
            {
                con.Open();
                string sql = "INSERT INTO Movies (MovieID, MovieTitle, MovieDesc, MovieDate, MovieRating) VALUES (@0, @1, @2, @3, @4)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("0", movie.id));
                cmd.Parameters.Add(new SqlParameter("1", movie.movieTitle));
                cmd.Parameters.Add(new SqlParameter("2", movie.movieDesc));
                cmd.Parameters.Add(new SqlParameter("3", movie.movieDate));
                cmd.Parameters.Add(new SqlParameter("4", movie.movieRating));
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteMovie(string id)
        {
            using (SqlConnection con = new SqlConnection(csMovie))
            {
                con.Open();
                string sql = "DELETE FROM Movies WHERE MovieID=@param1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("param1", id));
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateMovie(Movie movie, string id)
        {
            using (SqlConnection con = new SqlConnection(csMovie))
            {
                con.Open();
                string sql = "UPDATE Movies set MovieTitle=@1, MovieDesc=@2, MovieDate=@3, MovieRating=@4 WHERE MovieID=@0";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("0", id));
                cmd.Parameters.Add(new SqlParameter("1", movie.movieTitle));
                cmd.Parameters.Add(new SqlParameter("2", movie.movieDesc));
                cmd.Parameters.Add(new SqlParameter("3", movie.movieDate));
                cmd.Parameters.Add(new SqlParameter("4", movie.movieRating));
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}
