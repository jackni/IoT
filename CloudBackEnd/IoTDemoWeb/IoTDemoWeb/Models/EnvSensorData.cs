using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IoTDemoWeb.Models
{
    //https://msdn.microsoft.com/en-us/library/azure/dd179338.aspx
    public class EnvSensorData : TableEntity
    {
        #region Members
        [Required]
        public double Temperature { get; set; }//TODO changed to double!
        [Required]
        public double Humidity { get; set; }
        [Required]
        public double Pressure { get; set; }

        private string _deviceId;
        [Required]
        public string DeviceID
        {
            get { return this._deviceId; }
            set
            {
                this._deviceId = value;
                this._deviceId = this._deviceId.Replace(":", "").Replace("'", "");
                string timePartKey = DateTime.UtcNow.ToString(@"yyyyMMddHH");
                this.PartitionKey = this._deviceId + "_" + timePartKey;

                this.RowKey = Guid.NewGuid().ToString(); //TODO: this always can be optimized.
            }
        }

        public double Lat { get; set; }

        public double Lng { get; set; }

        #endregion

        #region Constructors
        public EnvSensorData()
        {
            //this.RowKey = Guid.NewGuid().ToString(); //TODO: this always can be optimized.
            //string timePartKey = DateTime.UtcNow.ToString(@"yyyyMMddHH");
            //this.PartitionKey = DeviceID + "_" + timePartKey;
        }

        //using device mac address & yyyy-mm-dd:hh as partionKey. so we can have average/hr value.
        //public EnvSensorData(string deviceId) //: this()
        //{
        //    this.DeviceID = deviceId;
        //    this.RowKey = Guid.NewGuid().ToString(); //TODO: this always can be optimized.
        //    string timePartKey = DateTime.UtcNow.ToString(@"yyyyMMddHH");
        //    this.PartitionKey = deviceId + "_" + timePartKey;
            
        //}
        #endregion
    }
}