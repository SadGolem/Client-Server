using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.StatementModel;

namespace ho.Pages.Home
{
    public class CreateStatementModel : PageModel
    {
        public StatementInfo statementInfo = new StatementInfo();
        public string errorMes = "";
        public string succsessMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            statementInfo.Type = Request.Form["Type"];
            statementInfo.Date = Request.Form["Date"];
            statementInfo.FIO = Request.Form["FIO"];
            statementInfo.Location = Request.Form["Location"];
            statementInfo.UUID = Request.Form["UUID"];

            if (statementInfo.UUID == "" || statementInfo.Type == "" || statementInfo.Date == ""
                || statementInfo.Location == "" || statementInfo.FIO == "")
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
                    String sql = "INSERT INTO Table_Statement " +
                        "(type_Statement, date_State_Create, str_FIO, loc_Creation, uuid) VALUES " +
                        "(@Type, @Date, @FIO, @Location, @UUID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Type", statementInfo.Type);
                        command.Parameters.AddWithValue("@Date", statementInfo.Date);
                        command.Parameters.AddWithValue("@FIO", statementInfo.FIO);
                        command.Parameters.AddWithValue("@Location", statementInfo.Location);
                        command.Parameters.AddWithValue("@UUID", statementInfo.UUID);

                        command.ExecuteNonQuery();
                    }
                }
                

            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return;
            }

            statementInfo.Type = ""; statementInfo.FIO = ""; statementInfo.Date = ""; statementInfo.Location = ""; statementInfo.UUID = "";
            succsessMessage = "New statement added correctly";

            Response.Redirect("/Home/Statement");
        }
    }
}
