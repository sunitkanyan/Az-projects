using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using System.Reflection.Metadata.Ecma335;

namespace HTTPTrigger
{/// <summary>
/// http trigger func for handshake of event grid validation
/// https://7a71-103-101-103-31.ap.ngrok.io/api/GetEvents
/// </summary>
    public static class GetEvents
    {
        [FunctionName("GetEvents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            StreamReader sr = new StreamReader(req.Body);
            string body = sr.ReadToEnd();



            log.LogInformation("C# HTTP trigger function processed a request {body}");
             EventGridSubscriber _sub
                 = new EventGridSubscriber();
            var evnts = _sub.DeserializeEventGridEvents(body);
            foreach (var evn in evnts)
            {
                if (evn.Data is SubscriptionValidationEventData)
                {
                    SubscriptionValidationEventData _data = (SubscriptionValidationEventData)evn.Data;
                    log.LogInformation(_data.ToString());

                    SubscriptionValidationResponse _response = new SubscriptionValidationResponse()
                    {
                        ValidationResponse = _data.ValidationCode
                    };
                    return new OkObjectResult(_response);
                }
            }
            return new OkObjectResult(string.Empty);
        }
    }
}
