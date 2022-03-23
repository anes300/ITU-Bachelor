using Model.Nodes;
using System;
using System.Net;

namespace Model.Messages
{
    public class Message 
    {  
        public Guid messageId { get; }
        public string messageBody { get; } // = PayloadBody
        public MessageType messageType { get; }
        public string senderIP { get; }
        public int senderPort { get; }

        public Message(string messageBody, MessageType messageType, string senderIP,int senderPort)
        {  
            this.messageId = Guid.NewGuid();
            this.messageBody = messageBody;
            this.messageType = messageType;
            this.senderIP = senderIP;
            this.senderPort = senderPort;
        }
    }

    public enum MessageType
    {       
        CONNECT=1,
        FORWARD=2,
        RESPONSEAPI=3,
        QUERY = 4,
    };
}