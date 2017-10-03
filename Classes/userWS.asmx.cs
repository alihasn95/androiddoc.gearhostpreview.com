using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Doc
{
    /// <summary>
    /// Summary description for userWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class userWS : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void Regester(string name, string password, int num)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            int ISAdded = 1;
            string Message = "";
            try
            {

                using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO patient(name,password,num) VALUES(@name,@password,@num)");
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@num", num);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    connection.Close();


                }
                Message = "your account seuccessfuly added";




            }
            catch (Exception ex)
            {
                ISAdded = 0;
                Message = ex.Message;
            }
            var jsonData = new
            {
                ISAdded = ISAdded,
            Message = Message
        };
        HttpContext.Current.Response.Write(ser.Serialize(jsonData));
        }


    }
}
