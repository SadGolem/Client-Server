using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static ho.Pages.Home.StatementModel;

namespace ho.Pages.Home
{
    public class ProfileModel : PageModel
    {
        public List<ProfileInfo> listProfile = new List<ProfileInfo>() { };
        public ProfileModel() { }
        public void OnGet()
        {
            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_ProfileTypes";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProfileInfo info = new ProfileInfo();
                                info.ID = "" + reader.GetInt32(0);
                                info.Name = reader.GetString(1);

                                listProfile.Add(info);
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

        public class ProfileInfo
        {
            public string ID;
            public string Name;
        }
    }
}
