using System;
using System.Net;
using System.Text.Json;
using Model.Messages;

namespace Server.Networking
{
	public class MessageHandler
	{
		List<IPEndPoint> nodeChildren;

		public MessageHandler()
		{
			nodeChildren = new List<IPEndPoint>();
		}

		public void HandleMessage(string message)
        {
			var msg = JsonSerializer.Deserialize<Message>(message);
						
			// MessageType
			switch(msg.messageType)
            {
				case MessageType.CONNECT:
                    {
						Console.WriteLine("MessageType: CONNECT");
						nodeChildren.Add(msg.sender);
						break;
                    }

				case MessageType.FORWARD:
					{
						Console.WriteLine("MessageType: FOWARD");

						// Serialize message
						var sendMsg = JsonSerializer.Serialize(msg);

						foreach (IPEndPoint child in nodeChildren)
                        {
							var sender = new NetworkSender(child, sendMsg);
							sender.SendMessage();
                        }

						break;
					}
				case MessageType.RESPONSEAPI:
					{

						Console.WriteLine("MessageType: RESPONSEAPI");
						break;
					}

				default:
					// TODO: Handle if no type is given
					break;		
            }


				// TODO: Handle if statements

			// TODO: Save ipAddress of Node to a list
			nodeChildren.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000)); //Replace ip and port


			// if messageType == toNodes
				// Send to list of RouteNodes
				// ExecutionPlan and DeploymentPlan

			// if messageType == RespondToUser
				// Send to User

			// if messageType == connectNode
				// Register new Node to TopologyManager
        }
	}
}

