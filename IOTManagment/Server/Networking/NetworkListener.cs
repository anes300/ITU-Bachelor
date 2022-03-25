using System;
using NetMQ;
using NetMQ.Sockets;
using Model.Messages;

namespace Server.Networking
{
	public class NetworkListener
	{
        private readonly MessageHandler _messageHandler;
        public NetworkListener(MessageHandler messageHandler)
        {
            _messageHandler = messageHandler; 
        }

		public void StartListener()
        {
            using (var listener = new PullSocket())
            {
                listener.Bind("tcp://0.0.0.0:6000");
                while (true)
                {
                    // Listen for messsages
                    string msg = listener.ReceiveFrameString();
                    Console.WriteLine("Received frame: {0}", msg);


                    // Handle the Message
                    _messageHandler.HandleMessage(msg);
                }
            }


        }
    }
}
