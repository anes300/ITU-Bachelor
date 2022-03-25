using System;
using System.Net;
using NetMQ;
using NetMQ.Sockets;
using Model.Messages;

namespace NodeEngine.Networking
{
	public class NetworkSender
	{
		IPEndPoint Receiver;
		string Message;

		public NetworkSender(IPEndPoint receiver, string message)
		{
			this.Receiver = receiver;
			this.Message = message;
		}

		public void SendMessage()
		{
			var sender = new PushSocket();
			
			Console.WriteLine("Connecting to socket...");
			sender.Connect($"tcp://{Receiver.Address}:{Receiver.Port}");

			sender.SendFrame(Message);

			Console.WriteLine("Message sent. Closing thread.");
			
		}
	}
}

