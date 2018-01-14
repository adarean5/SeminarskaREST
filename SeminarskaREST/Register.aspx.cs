using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SeminarskaREST.WebForms
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_name.Text = "";
                TB_username.Text = "";
                TB_password.Text = "";
            }
        }

        protected void B_register_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["dbmoviesConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
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

                Session["userId"] = userId;
                Session["userAdmin"] = false;
            }

            Response.Redirect("~/Dashboard.aspx");
        }

        protected void B_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }
    }
}