using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDeviceApp.Models
{
    public class DeviceData
    {
        public int DataID { get; set; }
        public string DeviceID { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Timestamp { get; set; }
    }
}