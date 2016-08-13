using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTDemoWeb.Models
{
    public class SensorDataDTO
    {
        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public float Pressure { get; set; }

        public string DeviceID { get; set; }
    }
}