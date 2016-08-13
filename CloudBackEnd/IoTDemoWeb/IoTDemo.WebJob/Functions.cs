using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using IoTDemoWeb.Models;
using IoTDemoWeb.Service;

namespace IoTDemo.WebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }


        public async static Task ProcessEnvSensorDataQueueAsync([QueueTrigger("queuesensordataasync")] EnvSensorData data, TextWriter logger)
        {
            IEnvSensorDataService envDataService;
            envDataService = new EnvSensorDataService();
            try
            {
                await Task.WhenAll(
                        envDataService.InsertSensorDataAsync(data)
                        , logger.WriteLineAsync("record procssed is:" + data.ToString())
                    );


            }
            catch (Exception ex)
            {
                await logger.WriteLineAsync("error encounter! error is:" + ex.Message);
                throw;
            }
        }
    }
}
