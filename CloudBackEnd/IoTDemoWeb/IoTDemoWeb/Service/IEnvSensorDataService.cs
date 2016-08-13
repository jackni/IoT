using IoTDemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDemoWeb.Service
{
    public interface IEnvSensorDataService
    {
        Task InsertSensorDataAsync(EnvSensorData data);
    }
}
