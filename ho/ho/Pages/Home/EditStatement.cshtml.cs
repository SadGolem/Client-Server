using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.StatementModel;

namespace ho.Pages.Home
{
    public class EditStatementModel : PageModel
    {
        public StatementInfo statementInfo = new StatementInfo();
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
                    string sql = "SELECT * FROM Table_Statement WHERE id_statement=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                statementInfo.ID = "" + reader.GetInt32(0);
                                statementInfo.Type = reader.GetString(1);
                                statementInfo.Date = reader.GetDateTime(2).ToString();
                                statementInfo.FIO = reader.GetString(3);
                                statementInfo.Location = reader.GetString(4);
                                statementInfo.UUID = "" + reader.GetGuid(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
            }
        }

        public void OnPost()
        {
            statementInfo.Type = Request.Form["Type"];
            statementInfo.Date = Request.Form["Date"];
            statementInfo.FIO = Request.Form["FIO"];
            statementInfo.Location = Request.Form["Location"];
            statementInfo.UUID = Request.Form["UUID"];

            if (statementInfo.UUID == "" || statementInfo.Type == "" || statementInfo.Date == ""
                || statementInfo.Location == "" || statementInfo.FIO == "" || statementInfo.ID == "")
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
                    String sql = "UPDATE Table_Statement " +
                        "SET type_Statement=@Type, date_State_Create=@Date, str_FIO=@FIO, loc_Creation=@Location, uuid=@UUID" +
                        "WHERE id_Statement=@id";
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
            succsessMessage = "Statement update correctly";

            Response.Redirect("/Home/Statement");

        }



    }
}
