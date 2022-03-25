using System;
using System.Net;
using System.Text.Json;
using Model.Messages;
using Model.Nodes;
using Model.Queries;
using Services;

namespace Server.Networking
{
	public class MessageHandler
	{
		List<IPEndPoint> nodeChildren;
		private readonly ITopologyManager _topologyManager;
		private IPEndPoint localIp = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 6000);	

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
								Console.WriteLine($"Node {msg.senderIP} reconnected.");
								break;
                            } 
							_topologyManager.AddNode(NodeEndPoint, node);
							nodeChildren.Add(NodeEndPoint);
							break;
						}

					case MessageType.FORWARD:
						{
							Console.WriteLine("MessageType: FORWARD");

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
							var varResults = JsonSerializer.Deserialize<List<SelectVariableResult>>(msg.messageBody);

                            foreach (var item in varResults)
                            {
								Console.WriteLine($"{item.Variable} has the value: {item.Value}");
                            }
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

		public void SendStop(Guid id)
        {
			string body = JsonSerializer.Serialize(id);
			Message msg = new Message(body, MessageType.STOP, localIp.Address.ToString(), localIp.Port);
			// Serialize message
			var sendMsg = JsonSerializer.Serialize(msg);

			foreach (IPEndPoint child in nodeChildren)
			{
				var sender = new NetworkSender(child, sendMsg);
				sender.SendMessage();
			}
		}

		public void SendQuery(Query query)
        {
			string body = JsonSerializer.Serialize(query);
			Message msg = new Message(body, MessageType.QUERY,localIp.Address.ToString(), localIp.Port);
			// Serialize message
			var sendMsg = JsonSerializer.Serialize(msg);

			foreach (IPEndPoint child in nodeChildren)
			{
				var sender = new NetworkSender(child, sendMsg);
				sender.SendMessage();
			}
		}
	}
}

