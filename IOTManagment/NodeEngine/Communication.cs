using System;
using NetMQ.Sockets;
using NetMQ;

namespace NodeEngine
{
	public class Communication
	{
		public static void Request(string input, RequestSocket client)
        {
			if (input != null)
            {
                Console.WriteLine("Sending message...");
				client.SendFrame(input.ToString());
            } else
            {
                Console.WriteLine("No message entered");
            }

            var msg = client.ReceiveFrameString();
            Console.WriteLine("Response: {0}", msg);
        }

        public static void Response(string input, ResponseSocket server)
        {
            if (input != null)
            {
                Console.WriteLine("Sending message...");
                server.SendFrame(input.ToString());
            }
            else
            {
                Console.WriteLine("No response entered");
            }
        }
    }
}

