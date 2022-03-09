using Model.Nodes;
using System;
using System.Net;

namespace Model.Messages
{
    public class Message 
    {  
        public Guid messageId { get; }
        public string message { get; }
        public MessageType messageType { get; }
        public string senderIP { get; }
        public int senderPort { get; }

        public Node? node { get; }
        

        public Message(Guid messageId, string message, MessageType messageType, string senderIP,int senderPort, Node? node)
        {  
            this.messageId = messageId;
            this.message = message;
            this.messageType = messageType;
            this.senderIP = senderIP;
            this.senderPort = senderPort;
            this.node = node;
        }
    }

    public enum MessageType
    {
        CONNECT=1,
        FORWARD=2,
        RESPONSEAPI=3
    };
}