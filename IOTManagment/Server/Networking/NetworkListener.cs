using System;
using NetMQ;
using NetMQ.Sockets;
using Model.Messages;

namespace Server.Networking
{
	public class NetworkListener
	{
        public NetworkListener()
        {
        }

		public void StartListener()
        {
            using (var listener = new ResponseSocket())
            {
                Console.WriteLine("Started Listening on port 6000...");
                listener.Bind("tcp://*:6000");
                
                while (true)
                {
                    // Listen for messsages
                    string msg = listener.ReceiveFrameString();
                    Console.WriteLine("Received frame: {0}", msg);


                    // Handle the Message
                    MessageHandler.HandleMessage(msg);
                }
            }

        }
    }
}

