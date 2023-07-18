using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class CreateDefectsModel : PageModel
    {
        public DefectsModel.DefectInfo defectstInfo = new DefectsModel.DefectInfo();
        public string errorMes = "";
        public string succsessMessage = "";
        public void OnGet()
        {

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
                String connectionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Table_DefectTypes " +
                        "(str_DefectName) VALUES " +
                        "(@Name)";
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
