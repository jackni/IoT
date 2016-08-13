using IoTDemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IoTDemoWeb.Service
{
    public class EnvSensorDataService : IEnvSensorDataService
    {
        #region Members

        #endregion

        #region Constructors
        public EnvSensorDataService()
        { }
        #endregion

        #region Methods
        public async Task InsertSensorDataAsync(EnvSensorData data)
        {
            try
            {
                using (var context = new DemoContext())
                {
                    await context.EnvSensorsData.AddAsync(data);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}