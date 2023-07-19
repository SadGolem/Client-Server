using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.DefectsModel;

namespace ho.Pages.Home
{
    public class EditDefectModel : PageModel
    {
        public DefectsModel.DefectInfo defectstInfo = new DefectsModel.DefectInfo();
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
                    string sql = "SELECT * FROM Table_DefectTypes WHERE id_DefectType=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                defectstInfo.ID = "" + reader.GetInt32(0);
                                defectstInfo.Name = reader.GetString(1);
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
            defectstInfo.Name = Request.Form["Name"];

            if (defectstInfo.Name == "")
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
                    String sql = "UPDATE Table_DefectTypes " +
                        "SET str_DefectName=@Name" +
                        "WHERE id_DefectType=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", defectstInfo.Name);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return;
            }

            defectstInfo.Name = "";
            succsessMessage = "New statement added correctly";

            Response.Redirect("/Home/Defects");
        }
    }
}
