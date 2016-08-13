using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IoTDemoWeb.Controllers
{
    public abstract class BaseController : ApiController
    {
        #region Members

        private CloudQueue _myQueue;

        public CloudQueue MyQueue { get { return _myQueue; } }

        #endregion

        #region Constructor
        public BaseController()
        {
            InitializeStorage();
        }
        #endregion

        #region Methods
        private void InitializeStorage()
        {
            try
            {
                // Open storage account using credentials from .cscfg file.
                var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());

                // Get context object for working with blobs, and 
                // set a default retry policy appropriate for a web user interface.
                //var blobClient = storageAccount.CreateCloudBlobClient();
                //blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

                // Get a reference to the blob container.
                //imagesBlobContainer = blobClient.GetContainerReference("images");

                // Get context object for working with queues, and 
                // set a default retry policy appropriate for a web user interface.
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                // Create the queue client

                queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

                // Get a reference to the queue.
                _myQueue = queueClient.GetQueueReference("queuesensordataasync"); //this is the queue methodname

                // Create the queue if it doesn't already exist

                _myQueue.CreateIfNotExists();
            }
            catch (StorageException storageEx)
            {
                throw storageEx;

            }
        }
        #endregion
    }
}
