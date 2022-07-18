using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.System.Diagnostics.DevicePortal;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.AppService;
using Windows.Web.Http;
using System.Net.Http;
using HttpClient = System.Net.Http.HttpClient;
using HttpResponseMessage = System.Net.Http.HttpResponseMessage;
using HttpMethod = Windows.Web.Http.HttpMethod;
using HttpRequestMessage = Windows.Web.Http.HttpRequestMessage;

namespace BTask
{
    public sealed class SampleProvider : IBackgroundTask
    {
        BackgroundTaskDeferral taskDeferral;
        DevicePortalConnection devicePortalConnection;



        BackgroundTaskCancellationReason _cancelReason = BackgroundTaskCancellationReason.Abort;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Take a deferral to allow the background task to continue executing 
            this.taskDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;

            // Create a DevicePortal client from an AppServiceConnection 
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            var appServiceConnection = details.AppServiceConnection;
            this.devicePortalConnection = DevicePortalConnection.GetForAppServiceConnection(appServiceConnection);

            // Add Closed, RequestReceived handlers 
            devicePortalConnection.Closed += DevicePortalConnection_Closed;
            devicePortalConnection.RequestReceived += DevicePortalConnection_RequestReceived;
        }

        private void DevicePortalConnection_Closed(DevicePortalConnection sender, DevicePortalConnectionClosedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            //_cancelRequested = true;
            _cancelReason = reason;

            Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
            //throw new NotImplementedException();
        }

        // Sample RequestReceived echo handler: respond with an HTML page including the query and some additional process information. 
        private void DevicePortalConnection_RequestReceived(DevicePortalConnection sender, DevicePortalConnectionRequestReceivedEventArgs args)
        {
            var req = args.RequestMessage;
            var res = args.ResponseMessage;

            if (req.RequestUri.AbsolutePath.EndsWith("/echo"))
            {

                // construct an html response message
                string con = "Vektor";
                //res.Content.Headers.Add("Allow", "GET");
                res.Content = new HttpStringContent(con);
                res.Content.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("text/html");
                res.StatusCode = HttpStatusCode.Ok;
            }

        }
    }
}