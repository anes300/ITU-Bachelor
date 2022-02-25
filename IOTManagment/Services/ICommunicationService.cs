using System;
using System.Net;
using Model.Messages;

namespace Services
{
	public interface ICommunicationService
	{

		void SendMessage(IPAddress ip, Message outgoing);
		void ReceiveMessage(Message incoming);
	}
}

