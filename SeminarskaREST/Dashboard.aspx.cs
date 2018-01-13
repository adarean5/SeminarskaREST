using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace SeminarskaREST
{
    public partial class Dashboard : System.Web.UI.Page
    {

        SqlConnection conn = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                PopulateTable();
        }

        private void PopulateTable()
        {
            string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);
            conn.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT [MovieID], [MovieTitle] AS Title, [MovieDesc] AS Description, [MovieDate] AS Date, [MovieRating] AS Rating FROM [dbo].[Movie]", conn);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                T_movies.DataSource = dt;
                T_movies.DataBind();
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                T_movies.DataSource = dt;
                T_movies.DataBind();
                T_movies.Rows[0].Cells.Clear();
                T_movies.Rows[0].Cells.Add(new TableCell());
                T_movies.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                T_movies.Rows[0].Cells[0].Text = "No Data Found...";
                T_movies.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

            conn.Close();
        }

        protected void AddNewMovie(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox TB_title = T_movies.FooterRow.FindControl("TB_movie_title_footer") as TextBox;
                TextBox TB_description = T_movies.FooterRow.FindControl("TB_movie_description_footer") as TextBox;
                TextBox TB_date = T_movies.FooterRow.FindControl("TB_movie_date_footer") as TextBox;
                TextBox TB_rating = T_movies.FooterRow.FindControl("TB_movie_rating_footer") as TextBox;

                string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Movie] (MovieTitle, MovieDesc, MovieDate, MovieRating) VALUES (@title, @desc, @date, @rating)", conn);
                cmd.Parameters.AddWithValue("@title", TB_title.Text);
                cmd.Parameters.AddWithValue("@desc", TB_description.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.Parse(TB_date.Text).Date);
                cmd.Parameters.AddWithValue("@rating", int.Parse(TB_rating.Text));

                cmd.ExecuteNonQuery();
                PopulateTable();

                L_movies_success.Text = "Movie successfully added!";
                L_movies_error.Text = "";
            }
            catch(Exception ex)
            {
                L_movies_success.Text = "";
                L_movies_error.Text = ex.Message;
            }
        }

        protected void EditMovie(object sender, GridViewEditEventArgs e)
        {
            T_movies.EditIndex = e.NewEditIndex;
            PopulateTable();
        }

        protected void DeleteMovie(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Movie] WHERE [MovieID] = @movieId", conn);
                cmd.Parameters.AddWithValue("@movieId", Convert.ToInt32(T_movies.DataKeys[e.RowIndex].Value.ToString()));

                cmd.ExecuteNonQuery();
                PopulateTable();

                L_movies_success.Text = "Movie successfully removed!";
                L_movies_error.Text = "";
            }
            catch (Exception ex)
            {
                L_movies_success.Text = "";
                L_movies_error.Text = ex.Message;
            }
        }

        protected void CancelEditMovie(object sender, GridViewCancelEditEventArgs e)
        {
            T_movies.EditIndex = -1;
            PopulateTable();
        }

        protected void UpdateMovie(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox TB_title = T_movies.Rows[e.RowIndex].FindControl("TB_movie_titler") as TextBox;
                TextBox TB_description = T_movies.Rows[e.RowIndex].FindControl("TB_movie_description") as TextBox;
                TextBox TB_date = T_movies.Rows[e.RowIndex].FindControl("TB_movie_date") as TextBox;
                TextBox TB_rating = T_movies.Rows[e.RowIndex].FindControl("TB_movie_rating") as TextBox;

                string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Movie] SET [MovieTitle] = @title, [MovieDesc] = @desc, [MovieDate] = @date, [MovieRating] = @rating WHERE [MovieID] = @movieId", conn);
                cmd.Parameters.AddWithValue("@title", TB_title.Text);
                cmd.Parameters.AddWithValue("@desc", TB_description.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.ParseExact(TB_date.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture).Date);
                cmd.Parameters.AddWithValue("@rating", int.Parse(TB_rating.Text));
                cmd.Parameters.AddWithValue("@movieId", Convert.ToInt32(T_movies.DataKeys[e.RowIndex].Value.ToString()));

                cmd.ExecuteNonQuery();
                T_movies.EditIndex = -1;
                PopulateTable();

                L_movies_success.Text = "Movie successfully updated!";
                L_movies_error.Text = "";
            }
            catch (Exception ex)
            {
                L_movies_success.Text = "";
                L_movies_error.Text = ex.Message;
            }
        }
    }
}