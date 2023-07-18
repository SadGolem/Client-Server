using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ho.Pages.Home
{
    public class CreateProductModel : PageModel
    {
        public ProductModel.ProductInfo productInfo = new ProductModel.ProductInfo();
        public string errorMes = "";
        public string succsessMessage = "";
        public void OnGet()
        {

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
                    String sql = "INSERT INTO Table_Products " +
                        "(str_Mark, list_KeyValues, id_ProfileType, id_MarkType, id_DefectInf, uuid) VALUES " +
                        "(@Mark, @KeyValues, @ProfileType, @MarkType, @DefectInfo, @UUID)";
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
            succsessMessage = "New statement added correctly";

            Response.Redirect("/Home/Product");
        }
    }
}

