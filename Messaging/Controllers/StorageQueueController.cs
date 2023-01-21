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
            QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=cloudshell1924384357;AccountKey=B/i63DLWD2+P7dIWmOiM9GACz8H7NG/PV1Pc6HAkOp9fqelrudFpwFyXYMgUacHm3RPLNpmTUQLQ+AStKn2p6w==;EndpointSuffix=core.windows.net", "storagequeue");
            await queueClient.CreateIfNotExistsAsync();

            await queueClient.SendMessageAsync(newmessage);
        }
    }
}
