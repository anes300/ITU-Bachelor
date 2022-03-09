using System;
using System.Net;

namespace Model.Messages
{
    public class Message 
    {  
        public Guid messageId { get; }
        public string message { get; }
        public MessageType messageType { get; }
        public IPEndPoint sender { get; }
        

        public Message(Guid messageId, string message, MessageType messageType, IPEndPoint sender)
        {  
            this.messageId = messageId;
            this.message = message;
            this.messageType = messageType;
            this.sender = sender;
            
        }
    }

    public enum MessageType
    {
        CONNECT=1,
        FORWARD=2,
        RESPONSEAPI=3,
        NODES
    };
}