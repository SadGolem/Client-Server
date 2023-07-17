using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class StatementModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";
        public List<StatementInfo> listState = new List<StatementInfo>() { };
        public StatementModel() { }
        public void OnGet()
        {
            Console.WriteLine(Message);
            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_Statement";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StatementInfo info = new StatementInfo();
                                info.ID = "" + reader.GetInt32(0);
                                info.Type = reader.GetString(1);
                                info.Date = reader.GetDateTime(2).ToString();
                                info.FIO = reader.GetString(3);
                                info.Location = reader.GetString(4);
                                info.UUID = "" + reader.GetGuid(5);

                                listState.Add(info);
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
        public string A()
        {
            return "A";
        }
        public class StatementInfo
        {
            public string? ID;
            public string? Type;
            public string? Date;
            public string? FIO;
            public string? Location;
            public string? UUID;
        }
    }
}
