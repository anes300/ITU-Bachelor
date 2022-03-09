using System;
using NetMQ.Sockets;
using NetMQ;

namespace NodeEngine.Networking
{
    public class NetworkListener
    {

        MessageHandler messageHandler;
        public NetworkListener()
        {
            messageHandler = new MessageHandler();
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
                    messageHandler.HandleMessage(msg);
                }
            }

        }
    }
}

