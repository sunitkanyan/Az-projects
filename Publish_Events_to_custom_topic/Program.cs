// See https://aka.ms/new-console-template for more information
using Azure.Messaging.EventGrid;
using System.Text.Json;

Console.WriteLine("Hello, World!");


EventGridPublisherClient _client = new EventGridPublisherClient(new Uri("endpoint from aventgrid"), new Azure.AzureKeyCredential("key"));

//event body

var message = new { name = 1212, Text = "text message" };


_client.SendEventAsync(new EventGridEvent("mysubject-add order", "app.neworderType", "1", JsonSerializer.Serialize(message)));

Console.WriteLine("message event send to custom topic");

