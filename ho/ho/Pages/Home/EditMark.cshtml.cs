using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.ProfileModel;

namespace ho.Pages.Home
{
    public class EditMarkModel : PageModel
    {
        public MarksModel.MarksInfo markstInfo = new MarksModel.MarksInfo();
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
                    string sql = "SELECT * FROM Table_MarksTypes WHERE id_MarkType=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                markstInfo.ID = "" + reader.GetInt32(0);
                                markstInfo.Name = reader.GetString(1);
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
            markstInfo.Name = Request.Form["Name"];

            if (markstInfo.Name == "")
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
                        "SET str_MarkName=@Name" +
                        "WHERE id_MarkType=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", markstInfo.Name);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return;
            }

            markstInfo.Name = "";
            succsessMessage = "Marks update correctly";

            Response.Redirect("/Home/Marks");
        }
    }
}
