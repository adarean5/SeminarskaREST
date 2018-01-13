using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeminarskaREST.WebForms
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection conn = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);
        }

        protected void B_register_Click(object sender, EventArgs e)
        {
            conn.Open();

            // Preveri, ali že obstaja uporabnik s takim uporabniškim imenom ali geslom
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[User] WHERE Username = @username OR Password = @password", conn);
            
            cmd.Parameters.AddWithValue("@username", TB_username.Text);
            cmd.Parameters.AddWithValue("@password", TB_password.Text);

            SqlDataReader result = cmd.ExecuteReader();

            // Nekaj od tega dvojega obstaja - ponovi vajo
            if (result.HasRows)
            {
                TB_name.Text = "";
                TB_username.Text = "";
                TB_password.Text = "";
                result.Close();
                conn.Close();
                return;
            }
            
            result.Close();

            // Dobi ID UserTypa "Ordinary"
            cmd = new SqlCommand("SELECT * FROM [dbo].[UserType] WHERE Title = 'Ordinary'", conn);
            result = cmd.ExecuteReader();
            result.Read();

            int userTypeID = result.GetInt32(result.GetOrdinal("UserTypeID"));

            result.Close();

            // Uporabnik še ne obstaja --> dodaj novega
            cmd = new SqlCommand("INSERT INTO [dbo].[User] (Name, UserTypeID, Username, Password) OUTPUT INSERTED.UserID VALUES (@name, @userTypeID, @username, @password)", conn);
            cmd.Parameters.AddWithValue("@name", TB_name.Text);
            cmd.Parameters.AddWithValue("@userTypeID", userTypeID);
            cmd.Parameters.AddWithValue("@username", TB_username.Text);
            cmd.Parameters.AddWithValue("@password", TB_password.Text);
            cmd.ExecuteNonQuery();

            // ID zadnjega inserta
            cmd = new SqlCommand("SELECT MAX(UserID) AS UserID FROM [dbo].[User]", conn);
            result = cmd.ExecuteReader();
            result.Read();

            int userId = result.GetInt32(result.GetOrdinal("UserID"));

            result.Close();
            conn.Close();

            Session["userId"] = userId;
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}