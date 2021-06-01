using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDeviceApp.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebDeviceApp.Controllers
{
    public class DeviceController : ApiController
    {
       
     public HttpResponseMessage Get()
    {
            System.Data.DataTable table = new System.Data.DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebDeviceApp"].ConnectionString);
            SqlCommand cmd = new SqlCommand("ReadDevices", con);
            using (var da = new SqlDataAdapter(cmd))
            {
               cmd.CommandType = CommandType.StoredProcedure;
              da.Fill(table);
             }
   
            return Request.CreateResponse(HttpStatusCode.OK, table);
    }
        public string Post(DeviceTable tableData)
        {
            try
            {
                System.Data.DataTable table = new System.Data.DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebDeviceApp"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DeviceInsert", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("DeviceID", tableData.DeviceID);
                cmd.Parameters.AddWithValue("DeviceName", tableData.DeviceName);
                cmd.Parameters.Add("@Flag", SqlDbType.Int, 1);
                cmd.Parameters["@Flag"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int k = (int) cmd.Parameters["@Flag"].Value;


                con.Close();
                if (k == 0)
                {
                    return "Addition Failed because Device of the ID already present"; 
                }
                else
                {
                    return "Added Successfully";
                }


            }
            catch (Exception)
            {
                return "Error Adding the device";
            }

        }


    }
}
