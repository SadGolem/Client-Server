using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class CreateDefectsInfoModel : PageModel
    {
        public DefectsInfoModel.DefectsInfo defectstInfo = new DefectsInfoModel.DefectsInfo();
        public string errorMes = "";
        public string succsessMessage = "";
        public void OnGet()
        {

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
                String connectionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Table_DefectInfo " +
                        "(str_DefectType, float_DefectLong) VALUES " +
                        "(@Type, @DefectLong)";
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
