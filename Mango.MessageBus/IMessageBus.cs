using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    // This interface represents a message bus, which is responsible for handling message publication.
    public interface IMessageBus
    {
        // Asynchronously publishes a message to a specified topic or queue.
        // The 'message' parameter is the object representing the message to be published.
        // The 'topic_queue_Name' parameter specifies the name of the topic or queue to which the message should be published.
        Task PublishMessage(object message, string topic_queue_Name);
    }
}

