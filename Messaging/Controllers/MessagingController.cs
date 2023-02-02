using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using System.Text;

namespace Messaging.Controllers
{
    [ApiController]
    [Route("messaging")]
    public class MessagingController : ControllerBase
    {
       public MessagingController()
        {
        }

        [HttpGet]
        [Route("sendtoqueue")]
        //https://localhost:7270/messaging/sendtoqueue?newmessage=ajdsa%20laksj%20dlajds%20lkajda
        public void SendMessage(string newmessage)
        {
            //queue name: salemessages
            var client = new ServiceBusClient("Endpoint=sb://salesteamappcontoso2022.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Zvug8C4/snwAyh/aDuw4K5rCKwnu7BUAt2BWicoTIOI=");
            client.CreateSender("salemessages").SendMessageAsync(new ServiceBusMessage(newmessage));

        }

        [HttpGet]
        [Route("receivemessage")]
        //https://localhost:7270/messaging/receivemessage
        public async Task<string> ReceiveMessage()
        {
            //queue name: salemessages
            var client = new ServiceBusClient("Endpoint=sb://salesteamappcontoso2022.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Zvug8C4/snwAyh/aDuw4K5rCKwnu7BUAt2BWicoTIOI=");
            var receiver = client.CreateReceiver("salemessages");
            var message = await receiver.ReceiveMessagesAsync(1);
            string messageRetrived = Encoding.ASCII.GetString(message.FirstOrDefault().Body);

            await receiver.CompleteMessageAsync(message.FirstOrDefault());

            return messageRetrived;

        }

        [HttpGet]
        [Route("SendTopicMessage")]
        //https://localhost:7270/messaging/SendTopicMessage?newmessage="{myproperty:1234}"
        public async void SendTopicMessage(string newmessage)        
        {
            //topic name:  salesperformancemessages
            var client = new ServiceBusClient("Endpoint=sb://kanyan-service-bus-test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rr/1/IWLip3+y4ZjrlPqNwVUyjpCbAeSwWAN/nkghXE=");
            var msg = new ServiceBusMessage(newmessage);
            msg.ApplicationProperties.Add("receiver", "corres"); //corres
            msg.Subject = "subject-test";
            await client.CreateSender("alwaysin").SendMessageAsync(msg);

        }


    }
}