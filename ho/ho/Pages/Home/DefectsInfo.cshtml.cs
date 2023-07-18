using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.DefectsModel;

namespace ho.Pages.Home
{
    public class DefectsInfoModel : PageModel
    {
        public List<DefectsInfo> listDefectsInfo = new List<DefectsInfo>() { };
        public DefectsInfoModel() { }
        public void OnGet()
        {
            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_DefectInfo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DefectsInfo info = new DefectsInfo();
                                info.ID = "" + reader.GetInt32(0);
                                info.Type = "" + reader.GetInt32(1);
                                info.DefectLong = reader.GetDouble(2).ToString();

                                listDefectsInfo.Add(info);
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

        public class DefectsInfo
        {
            public string ID;
            public string Type;
            public string DefectLong;
        }
    }
}
