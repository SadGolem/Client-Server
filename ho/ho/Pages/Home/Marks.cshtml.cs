using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class MarksModel : PageModel
    {
        public List<MarksInfo> listMarks = new List<MarksInfo>() { };
        public MarksModel() { }
        public void OnGet()
        {
            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_MarksTypes";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MarksInfo info = new MarksInfo();
                                info.ID = "" + reader.GetInt32(0);
                                info.Name = reader.GetString(1);

                                listMarks.Add(info);
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

        public class MarksInfo
        {
            public string ID;
            public string Name;
        }
    }
}
