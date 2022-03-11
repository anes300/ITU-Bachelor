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
                Console.WriteLine("Started Listening on port 6000...");
                listener.Bind("tcp://127.0.0.1:6000");
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
