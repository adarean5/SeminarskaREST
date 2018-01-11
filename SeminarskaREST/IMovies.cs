using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SeminarskaREST
{
    [ServiceContract]
    public interface IMovies
    {
        [OperationContract]
        [WebGet(UriTemplate = "Movies", ResponseFormat = WebMessageFormat.Json)]
        List<Movie> GetMovieList();

        [OperationContract]
        [WebGet(UriTemplate = "Movie/{id}", ResponseFormat = WebMessageFormat.Json)]
        Movie GetMovie(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Movie", ResponseFormat = WebMessageFormat.Json)]
        void AddMovie(Movie movie);

        [OperationContract]
        [WebInvoke(UriTemplate = "Movie/{id}", ResponseFormat = WebMessageFormat.Json, Method = "DELETE")]
        void DeleteMovie(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Movie/{id}", ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        void UpdateMovie(Movie movie, string id);

        [OperationContract]
        [WebGet(UriTemplate = "Login", ResponseFormat = WebMessageFormat.Json)]
        void Authenticate();
    }

    [DataContract]
    public class Movie
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string movieTitle { get; set; }
        [DataMember]
        public string movieDesc { get; set; }
        [DataMember]
        public string movieDate { get; set; }
        [DataMember]
        public double movieRating { get; set; }
    }
}
