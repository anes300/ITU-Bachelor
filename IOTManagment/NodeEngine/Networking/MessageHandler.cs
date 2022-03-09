using System;
using System.Net;
using System.Text.Json;
using Model.Messages;


namespace NodeEngine.Networking
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
			try
			{
				var msg = JsonSerializer.Deserialize<Message>(message);

				// MessageType
				switch (msg.messageType)
				{
					case MessageType.CONNECT:
						{
							Console.WriteLine("MessageType: CONNECT");
							var node = new IPEndPoint(IPAddress.Parse(msg.senderIP), msg.senderPort);
							if (nodeChildren.Any(x => x.Address.Equals(IPAddress.Parse(msg.senderIP))))
							{
								Console.WriteLine("Node already exists.");
								break;
							}
							nodeChildren.Add(node);
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
			catch (Exception e)
			{
				Console.WriteLine($"Could not deserialize. Message: {message}");
				Console.WriteLine($"Exception: {e}");
			}
		}
	}
}

