using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.DefectsInfoModel;

namespace ho.Pages.Home
{
    public class EditDefectInfoModel : PageModel
    {
        public DefectsInfoModel.DefectsInfo defectstInfo = new DefectsInfoModel.DefectsInfo();
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
                    string sql = "SELECT * FROM Table_DefectInfo WHERE id_DefectInf=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                defectstInfo.ID = "" + reader.GetInt32(0);
                                defectstInfo.Type = "" + reader.GetInt32(1);
                                defectstInfo.DefectLong = reader.GetDouble(2).ToString();
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
            defectstInfo.Type = Request.Form["Type"];
            defectstInfo.DefectLong = Request.Form["DefectLong"];

            if (defectstInfo.Type == "" || defectstInfo.DefectLong == "")
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
                    String sql = "UPDATE Table_DefectInfo " +
                        "SET id_DefectType=@Type, float_DefectLong=@DefectLong" +
                        "WHERE id_DefectInf=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Type", defectstInfo.Type);
                        command.Parameters.AddWithValue("@DefectLong", defectstInfo.DefectLong);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return;
            }

            defectstInfo.Type = ""; defectstInfo.DefectLong = "";
            succsessMessage = "New defectsInfo added correctly";

            Response.Redirect("/Home/DefectsInfo");
        }
    }
}
