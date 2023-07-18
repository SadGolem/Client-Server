using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class CreateMarksTypesModel : PageModel
    {
        public MarksModel.MarksInfo markstInfo = new MarksModel.MarksInfo();
        public string errorMes = "";
        public string succsessMessage = "";
        public void OnGet()
        {

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
                String connectionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Table_MarksTypes " +
                        "(str_MarkName) VALUES " +
                        "(@Name)";
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
            succsessMessage = "New statement added correctly";

            Response.Redirect("/Home/Marks");
        }
    }
}
