using System;
using System.Net;
using Model.Messages;

namespace Services
{
    public class CommunicationService : ICommunicationService
    {
        public void SendMessage(IPAddress ip, Message outgoing)
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessage(Message incoming)
        {
            throw new NotImplementedException();
        }
    }
}
