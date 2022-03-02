using System;
using System.Net;
using NetMQ;
using NetMQ.Sockets;
using Model.Messages;

namespace Server.Networking
{
	public class NetworkSender
	{
		IPEndPoint receiver;
		Message message;

		public NetworkSender(IPEndPoint receiver, string message)
		{
			this.receiver = receiver;
			this.message = new Message(new Guid(), message);
		}

		public void SendMessage()
        {
			using (var sender = new RequestSocket())
            {
				Console.WriteLine("Connecting to socket...");
				sender.Connect($"tcp://{receiver.Address}:{receiver.Port}");

				sender.SendFrame(message.message);

				Console.WriteLine("Message sent. Closing thread.");
            }
        }
	}
}

