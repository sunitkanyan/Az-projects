// See https://aka.ms/new-console-template for more information
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;
using System.Text;

Console.WriteLine("Hello, World!");

List<Order> _orders = new List<Order>()
{
  new Order () { Orderid=1,Quantity =10, UnitPrice=10,discount =1},
    new Order () { Orderid=3,Quantity =140, UnitPrice=5,discount =1},
      new Order () { Orderid=5,Quantity =1, UnitPrice=70,discount =1},
        new Order () { Orderid=7,Quantity =100, UnitPrice=80,discount =1},

}
;


EventHubProducerClient _producerClient = new EventHubProducerClient("Endpoint=sb://skanyaneventhub.servicebus.windows.net/;SharedAccessKeyName=sendlist;SharedAccessKey=4V1L3wZP9BmD0Xg05YAlqhywToTuohuRyB44HRf1AK4=;EntityPath=myevent");

EventDataBatch _batch = _producerClient.CreateBatchAsync().GetAwaiter().GetResult();

foreach (Order _order in _orders)
    _batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_order))));

_producerClient.SendAsync(_batch).GetAwaiter().GetResult();