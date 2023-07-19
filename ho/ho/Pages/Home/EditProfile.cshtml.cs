using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.ProfileModel;

namespace ho.Pages.Home
{
    public class EditProfileModel : PageModel
    {
        public ProfileModel.ProfileInfo profiletInfo = new ProfileModel.ProfileInfo();
        public string errorMes = "";
        public string succsessMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_ProfileTypes WHERE id_ProfileType=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                profiletInfo.ID = "" + reader.GetInt32(0);
                                profiletInfo.Name = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void OnPost()
        {
            profiletInfo.Name = Request.Form["Name"];

            if (profiletInfo.Name == "")
            {
                errorMes = "Null";
                return;
            }

            try
            {
                String connectionString = "Data Source=HOME-PC;Initial Catalog=bd;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Table_ProfileTypes " +
                        "SET str_ProfileName=@Name" +
                        "WHERE id_ProfileType=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", profiletInfo.Name);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return;
            }

            profiletInfo.Name = "";
            succsessMessage = "Profile update correctly";

            Response.Redirect("/Home/Profile");
        }
    }
}
