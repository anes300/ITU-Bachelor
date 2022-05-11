using System;
using System.Net;
using NetMQ;
using NetMQ.Sockets;
using Model.Messages;

namespace NodeEngine.Networking
{
	public class NetworkSender
	{
		PushSocket sender;
		IPEndPoint oldReceiver = default;
		public NetworkSender()
		{
			sender = new PushSocket();
		}

		public void SendMessage(IPEndPoint receiver, string message)
		{
           
			if (oldReceiver != default)
            {
			sender.Disconnect($"tcp://{oldReceiver}");

			}

			oldReceiver = receiver;
			Console.WriteLine("Connecting to socket...");
			sender.Connect($"tcp://{receiver.Address}:{receiver.Port}");

			sender.SendFrame(message);
			
			Console.WriteLine("Message sent. Closing thread.");

			
			

		}
	}
}

