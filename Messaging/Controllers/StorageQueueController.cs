using Azure.Messaging.ServiceBus;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageQueueController : ControllerBase
    {
        [HttpGet]
        [Route("send")]
        //https://localhost:7270/api/StorageQueue/send?newmessage=ajdsa%20laksj%20dlajds%20lkajda
        public async Task SendMessageAsync(string newmessage)
        {
            //queue name: storagequeue
            QueueClient queueClient = new QueueClient("Endpoint=sb://kanyan-service-bus-test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rr/1/IWLip3+y4ZjrlPqNwVUyjpCbAeSwWAN/nkghXE=", "storagequeue");
            await queueClient.CreateIfNotExistsAsync();
            ServiceBusMessage message = new ServiceBusMessage(newmessage);
            message.ApplicationProperties.Add("receiver", "sql"); //corres
            message.Subject = "subject-test";
            await queueClient.SendMessageAsync(newmessage);
        }
    }
}
