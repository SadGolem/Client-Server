using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class ProductModel : PageModel
    {
        public List<ProductInfo> listProduct = new List<ProductInfo>() { };
        public ProductModel() { }
        public void OnGet()
        {
            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_Products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo info = new ProductInfo();
                                info.Mark = reader.GetString(0);
                                info.KeyValues = reader.GetString(1);
                                info.ProfileType = "" + reader.GetInt32(2);
                                info.MarkType = "" + reader.GetInt32(3);
                                info.DefectInfo = "" + reader.GetInt32(4);
                                info.UUID = "" + reader.GetGuid(5);

                                listProduct.Add(info);
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

        public class ProductInfo
        {
            public string Mark;
            public string KeyValues;
            public string ProfileType;
            public string MarkType;
            public string DefectInfo;
            public string UUID;
        }
    }
}
