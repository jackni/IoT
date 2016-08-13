using IoTDemoWeb.Infrastructure;
using IoTDemoWeb.Models;
using IoTDemoWeb.Service;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IoTDemoWeb.Controllers
{
    [Authorize]
    [ApiExceptionFilter]
    public class EnvSensorController : BaseController
    {
        #region Members
        private IEnvSensorDataService _sensorDataService;
        #endregion

        #region Constructors
        public EnvSensorController(IEnvSensorDataService sensorDataService) :base()
        {
            
            _sensorDataService = sensorDataService;
        }
        #endregion

        #region API Action Methods

        [HttpPost]
        [ValidationActionFilter]
        [Route("api/MySensor/UploadSensorData")]
        public async Task<IHttpActionResult> UploadEnvSensorData([FromBody] EnvSensorData data)//
        {
            try
            {
                //mapping 
                //EnvSensorData sensorData = new EnvSensorData(data.DeviceID);
                //sensorData.Temperature = data.Temperature.ToString();
                //sensorData.Humidity = data.Humidity.ToString();
                //sensorData.Pressure = data.Pressure.ToString();


                //await _sensorDataService.InsertSensorDataAsync(sensorData);
                //push to Azure queue
                var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(data));
                await this.MyQueue.AddMessageAsync(queueMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        #endregion
    }
}
