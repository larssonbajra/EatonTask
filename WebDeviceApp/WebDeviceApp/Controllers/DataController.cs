using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDeviceApp.Models;

namespace WebDeviceApp.Controllers
{
    public class DataController : ApiController
    {
        public HttpResponseMessage Get()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebDeviceApp"].ConnectionString);
            SqlCommand cmd = new SqlCommand("CountRecords", con);
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public HttpResponseMessage Get(string deviceId)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebDeviceApp"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PrintRecords", con);
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("DeviceID", deviceId);
                da.Fill(table);
            }            
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(DeviceData datas)
        {
            try
            {
                System.Data.DataTable table = new System.Data.DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebDeviceApp"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DataInsert", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("DeviceID", datas.DeviceID);
                cmd.Parameters.AddWithValue("Temp", datas.Temperature);
                cmd.Parameters.AddWithValue("Humidity", datas.Humidity);
                cmd.Parameters.Add("@Flag", SqlDbType.Int, 1);
                cmd.Parameters["@Flag"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int k = (int)cmd.Parameters["@Flag"].Value;
                con.Close();
                if (k == 1)
                {
                    return "Added Successfully";
                }
                else
                {
                    return "Addition Failed because no Device of the ID found";
                }
                
              
            }
            catch (Exception)
            {
                return "Error Adding the data";
            }
            
        }

    }
}
