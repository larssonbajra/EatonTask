using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebDeviceApp.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            DataTable devData = new DataTable();
            devData.Columns.Add("DeviceID");
            devData.Columns.Add("Temperature");
            devData.Columns.Add("Timestamp");

            devData.Rows.Add(1, 12, 1990-12-12);
            return Request.CreateResponse(HttpStatusCode.OK, devData);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
