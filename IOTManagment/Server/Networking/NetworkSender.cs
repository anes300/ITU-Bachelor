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
		string message;

		public NetworkSender(IPEndPoint receiver, string message)
		{
			this.receiver = receiver;
			this.message = message;
		}

		public void SendMessage()
        {
			var sender = new PushSocket();
           
			Console.WriteLine("Connecting to socket...");
			sender.Connect($"tcp://{receiver.Address}:{receiver.Port}");

			sender.SendFrame(message);

			Console.WriteLine("Message sent. Closing thread.");
            
        }
	}
}

