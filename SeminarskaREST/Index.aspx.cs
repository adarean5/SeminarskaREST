using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SeminarskaREST
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TB_username.Text = "";
                TB_password.Text = "";
            }
            
        }

        protected void B_login_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT * FROM [dbo].[User] WHERE Username = @username AND Password = @password";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@username", TB_username.Text);
                cmd.Parameters.AddWithValue("@password", TB_password.Text);

                SqlDataReader result = cmd.ExecuteReader();

                if (!result.HasRows)
                {
                    TB_username.Text = "";
                    TB_password.Text = "";
                    result.Close();
                    conn.Close();
                    return;
                }

                result.Read();

                int userId = result.GetInt32(result.GetOrdinal("UserID"));

                result.Close();
                conn.Close();

                Session["userId"] = userId;
            }
            
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void B_register_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }   
    }
}