// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
//use ngrok for webhook for localhost
//https://7a71-103-101-103-31.ap.ngrok.io/runtime/webhooks/EventGrid?functionName=Function1
//download ngrok and run auth command for your ng account and set http port.

using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace WebhookEventGrid
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Topic);

            log.LogInformation(eventGridEvent.Subject);
            log.LogInformation(eventGridEvent.DataVersion);
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
