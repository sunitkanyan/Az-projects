// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

Console.WriteLine("Hello, World!");

ServiceBusClient _client = new ServiceBusClient("Endpoint=sb://skkbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yxv+t8GEqtdtyMArIL4vzqkCOazCLagrk4vN6S9QRHQ=");
var _sender =_client.CreateSender("myq");

ServiceBusMessage _message = new ServiceBusMessage("this is my text message for this queue");
_message.ApplicationProperties.Add("Dept", "IT");
_message.MessageId = "1213";

_sender.SendMessageAsync(new ServiceBusMessage("this is my text message for this queue"));
Console.ReadLine();