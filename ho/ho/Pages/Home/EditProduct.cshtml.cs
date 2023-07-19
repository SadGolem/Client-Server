using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ho.Pages.Home
{
    public class EditProductModel : PageModel
    {
        public ProductModel.ProductInfo productInfo = new ProductModel.ProductInfo();
        public string errorMes = "";
        public string succsessMessage = "";

        public void OnGet()
        {
            String UUID = Request.Query["id"];

            try
            {
                string conncetionString = "Data Source=DESKTOP-AVLULP7;Initial Catalog=EVRAZ_NIR_DB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Table_Products WHERE uuid=@UUID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("UUID", UUID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productInfo.Mark = reader.GetString(0);
                                productInfo.KeyValues = reader.GetString(1);
                                productInfo.ProfileType = "" + reader.GetInt32(2);
                                productInfo.MarkType = "" + reader.GetInt32(3);
                                productInfo.DefectInfo = "" + reader.GetInt32(4);
                                productInfo.UUID = "" + reader.GetGuid(5);
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
        public void OnPost()
        {
            productInfo.Mark = Request.Form["Mark"];
            productInfo.KeyValues = Request.Form["KeyValues"];
            productInfo.ProfileType = Request.Form["ProfileType"];
            productInfo.MarkType = Request.Form["MarkType"];
            productInfo.DefectInfo = Request.Form["DefectInfo"];
            productInfo.UUID = Request.Form["UUID"];

            if (productInfo.UUID == "" || productInfo.Mark == "" || productInfo.KeyValues == ""
                || productInfo.ProfileType == "" || productInfo.MarkType == "" || productInfo.DefectInfo == "")
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
                    String sql = "UPDATE Table_Products " +
                        "SET str_Mark=@Mark, list_KeyValues=@KeyValue, id_ProfileType=@ProfileType, id_MarkType=@MarkType, id_DefectInf=@DefectInfo, uuid=@UUID" +
                        "WHERE uuid=@UUID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Mark", productInfo.Mark);
                        command.Parameters.AddWithValue("@KeyValue", productInfo.KeyValues);
                        command.Parameters.AddWithValue("@ProfileType", productInfo.ProfileType);
                        command.Parameters.AddWithValue("@MarkType", productInfo.MarkType);
                        command.Parameters.AddWithValue("@DefectInfo", productInfo.DefectInfo);
                        command.Parameters.AddWithValue("@UUID", productInfo.UUID);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return;
            }

            productInfo.Mark = ""; productInfo.KeyValues = ""; productInfo.ProfileType = ""; productInfo.MarkType = ""; productInfo.DefectInfo = ""; productInfo.UUID = "";
            succsessMessage = "Product update correctly";

            Response.Redirect("/Home/Product");
        }
    }
}
