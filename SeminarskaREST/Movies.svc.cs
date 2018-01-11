using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Net;

namespace SeminarskaREST
{
    public class Movies : IMovies
    {
        string csMovie = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
        private List<Tuple<string, string>> validLogins = new List<Tuple<string, string>> {
            new Tuple<string, string>("admin", "test"),
            new Tuple<string, string>("jernej", "strazisar"),
            new Tuple<string, string>("klementina", "garbajs")
        }; 

        private bool AuthenticateUser()
        {
            WebOperationContext ctx = WebOperationContext.Current;
            string authHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (authHeader == null)
                return false;

            string[] loginData = authHeader.Split(':');
            if (loginData.Length == 2 && Login(loginData[0], loginData[1]))
                return true;
            return false;
        }

        public bool Login(string username, string password)
        {
            if (validLogins.Contains(new Tuple<string, string>(username, password)) )
                return true;
            return false;
        }

        public void Authenticate()
        {
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");
        }

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
                            movieRating = reader.GetDouble(4)
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
                        movie.movieRating = reader.GetDouble(4);
                    }
                }
                con.Close();
            }
            return movie;
        }

        public void AddMovie(Movie movie)
        {
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

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
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

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
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

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
