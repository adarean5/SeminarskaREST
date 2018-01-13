using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SeminarskaREST
{
    public partial class Dashboard : System.Web.UI.Page
    {

        SqlConnection conn = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);

                conn.Open();
                
                SqlDataAdapter sda = new SqlDataAdapter("SELECT [MovieTitle] AS Title, [MovieDesc] AS Description, [MovieDate] AS Date, [MovieRating] AS Rating FROM [dbo].[Movie]", conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                T_movies.DataSource = dt;
                T_movies.DataBind();
                
                conn.Close();
            }
        }
    }
}