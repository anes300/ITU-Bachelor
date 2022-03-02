using System;

namespace Model.Messages
{
    public class Message 
    {  
        private Guid messageId;
        public string message { get; }
        public Message(Guid messageId, string message)
        {  
            this.messageId = messageId;
            this.message = message;
        }

    }
}