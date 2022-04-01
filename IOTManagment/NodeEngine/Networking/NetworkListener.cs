using System;
using NetMQ.Sockets;
using NetMQ;

namespace NodeEngine.Networking
{
    public class NetworkListener
    {

        MessageHandler messageHandler;
        public NetworkListener(MessageHandler handler)
        {
            messageHandler = handler;
        }

        public void StartListener(int port)
        {
            using (var listener = new PullSocket())
            {
                Console.WriteLine($"Started Listening on port {port}...");
                listener.Bind($"tcp://0.0.0.0:{port}");

                while (true)
                {
                    // Listen for messsages
                    string msg = listener.ReceiveFrameString();
                    Console.WriteLine("Received frame: {0}", msg);


                    // Handle the Message
                    messageHandler.HandleMessage(msg);
                }
            }

        }
    }
}

