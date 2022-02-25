using System;

namespace Model.Messages
{
    public class Message 
    {  
        private Guid messageId;
        private String message;
        public Message(Guid messageId, string message)
        {  
            this.messageId = messageId;
            this.message = message;
        }

    }
}