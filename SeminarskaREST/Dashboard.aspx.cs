using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SeminarskaREST
{
    public partial class Dashboard : System.Web.UI.Page
    {

        SqlConnection conn = null;
        public bool UserAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Session["userId"] == null)
                {
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    UserAdmin = Convert.ToBoolean(Session["userAdmin"]);
                    PopulateTable();
                }
            }
        }

        private void PopulateTable()
        {
            string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
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
            }
        }

        protected void AddNewMovie(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("AddNew"))
            {
                try
                {
                    TextBox TB_title = T_movies.FooterRow.FindControl("TB_movie_title_footer") as TextBox;
                    TextBox TB_description = T_movies.FooterRow.FindControl("TB_movie_description_footer") as TextBox;
                    TextBox TB_date = T_movies.FooterRow.FindControl("TB_movie_date_footer") as TextBox;
                    TextBox TB_rating = T_movies.FooterRow.FindControl("TB_movie_rating_footer") as TextBox;

                    string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Movie] (MovieTitle, MovieDesc, MovieDate, MovieRating) VALUES (@title, @desc, @date, @rating)", conn);
                        cmd.Parameters.AddWithValue("@title", TB_title.Text);
                        cmd.Parameters.AddWithValue("@desc", TB_description.Text);
                        cmd.Parameters.AddWithValue("@date", DateTime.ParseExact(TB_date.Text, "yyyy-M-d", null));
                        cmd.Parameters.AddWithValue("@rating", int.Parse(TB_rating.Text));

                        cmd.ExecuteNonQuery();
                        PopulateTable();

                        L_movies_success.Text = "Movie successfully added!";
                        L_movies_error.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    L_movies_success.Text = "";
                    L_movies_error.Text = ex.Message;
                }
            }
        }
        
        protected void B_logout_Click(object sender, EventArgs e)
        {
            Session["userId"] = null;
            Session["userAdmin"] = null;
            Response.Redirect("~/Index.aspx");
        }
    }
}