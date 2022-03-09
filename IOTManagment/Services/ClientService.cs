using System;
using NetMQ;
using NetMQ.Sockets;

namespace Services
{
	public class ClientService
	{
        public ClientService()
        {

        }

		public void BuildClient()
        {
            using (var client = new RequestSocket())
            {
                Console.WriteLine("Starting Client...");
                client.Connect("tcp://127.0.0.1:6000");
                client.SendFrame("Hello");
                var msg = client.ReceiveFrameString();
                Console.WriteLine("From Server: {0}", msg);
                //client.ReceiveFrameString();
            }
        }
	}
}

