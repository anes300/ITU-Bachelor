using System;
using System.Net;
using System.Text.Json;
using Model.Messages;
using Model.Nodes;
using Services;

namespace Server.Networking
{
	public class MessageHandler
	{
		List<IPEndPoint> nodeChildren;
		private readonly ITopologyManager _topologyManager;
		public MessageHandler(ITopologyManager topologyManager)
		{
			_topologyManager = topologyManager;
			nodeChildren = new List<IPEndPoint>();
		}

		public void HandleMessage(string message)
        {
			Console.WriteLine("Received something");
			try
            {
				var msg = JsonSerializer.Deserialize<Message>(message);

				// MessageType
				switch (msg.messageType)
				{
					case MessageType.CONNECT:
						{
							Console.WriteLine("MessageType: CONNECT");
							var node = JsonSerializer.Deserialize<Node>(msg.messageBody);
							var NodeEndPoint = new IPEndPoint(IPAddress.Parse(node.Address), node.AddressPort);
							if (nodeChildren.Any(x => x.Address.Equals(IPAddress.Parse(msg.senderIP))))
                            {
								Console.WriteLine("Node already exists.");
								break;
                            } 
							_topologyManager.AddNode(NodeEndPoint, node);
							nodeChildren.Add(NodeEndPoint);
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
						Console.WriteLine("No msgType" + msg);
						break;
				}
			}
			catch
            {
				Console.WriteLine($"Could not deserialize. Message: {message}");
            }
        }
	}
}

