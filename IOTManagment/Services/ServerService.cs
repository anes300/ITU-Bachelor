using System;
using NetMQ;
using NetMQ.Sockets;
using Model.Messages;

namespace Services
{
	public class ServerService
	{
        public ServerService()
        {
        }

		public void BuildServer()
        {
            using (var server = new ResponseSocket())
            {
                Console.WriteLine("Starting Server...");
                server.Bind("tcp://*:6000");
                var communcationService = new CommunicationService();
                while (true)
                {
                    // Receive
                    //var msg = server.ReceiveFrameString();
                    //Message message = new Message(new Guid(), msg);
                    //communcationService.ReceiveMessage(message);

                    // Response to sender
                    //server.SendFrame("hello");

                    string msg = server.ReceiveFrameString();
                    Console.WriteLine("From Client: {0}", msg);
                    server.SendFrame("World");
                }
            }
        }
	}
}

