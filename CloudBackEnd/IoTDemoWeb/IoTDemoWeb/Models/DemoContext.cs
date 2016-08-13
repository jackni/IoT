using AzureStorage.Context;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTDemoWeb.Models
{
    public class DemoContext : TableContext
    {
        #region Members
        public TableSet<EnvSensorData> EnvSensorsData { get; set; }
        #endregion

        #region Constructors
        public DemoContext() : base("TableStorageContext")
        {
            TableCreating(base.TableClient);
        }
        #endregion

        #region Methods
        /// <summary>
        /// This is similar to the ModelBuilder
        /// </summary>
        /// <param name="tableClient"></param>
        public override void TableCreating(CloudTableClient tableClient)
        {
            EnvSensorsData = new TableSet<EnvSensorData>();
            var todoTable = tableClient.GetTableReference("EnvSensorsData");
            EnvSensorsData.Table = todoTable;
            todoTable.CreateIfNotExists();
        }
        #endregion
    }
}