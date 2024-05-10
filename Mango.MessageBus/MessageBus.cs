using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    // This class implements the IMessageBus interface and provides functionality for publishing messages to Azure Service Bus.
    public class MessageBus : IMessageBus
    {
        // The connection string used to connect to Azure Service Bus.
        private string connectionString = "";

        // Publishes a message to a specified topic or queue in Azure Service Bus.
        // The 'message' parameter is the object representing the message to be published.
        // The 'topic_queue_Name' parameter specifies the name of the topic or queue to which the message should be published.
        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            // Create a new ServiceBusClient instance using the specified connection string.
            await using var client = new ServiceBusClient(connectionString);

            // Create a ServiceBusSender for the specified topic or queue.
            ServiceBusSender sender = client.CreateSender(topic_queue_Name);

            // Serialize the message object to JSON format.
            var jsonMessage = JsonConvert.SerializeObject(message);

            // Create a ServiceBusMessage with the serialized JSON message payload.
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                // Set a unique CorrelationId for the message.
                CorrelationId = Guid.NewGuid().ToString()
            };

            // Send the message asynchronously to the specified topic or queue using the sender.
            await sender.SendMessageAsync(finalMessage);

            // Dispose of the ServiceBusClient instance after sending the message.
            await client.DisposeAsync();
        }
    }
}

