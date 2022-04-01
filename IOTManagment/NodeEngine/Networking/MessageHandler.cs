using System;
using System.Net;
using System.Text.Json;
using Model.Messages;
using Model.Queries;
using NodeEngine.Services;
using Serilog;

namespace NodeEngine.Networking
{
	public class MessageHandler
	{
		List<IPEndPoint> nodeChildren;
		QueryScheduler scheduler;
		IPEndPoint parentEndPoint;
		NetworkSender sender;
		public MessageHandler(IPEndPoint parent)
		{
			nodeChildren = new List<IPEndPoint>();
			scheduler = new QueryScheduler();
			parentEndPoint = parent;
			sender = new NetworkSender();
		}

		public async void HandleMessage(string message)
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

							Console.WriteLine("Sending Topology message");

							//Send new message with info about new node to topology manager in server.
							var connectionMsg = new Message(msg.messageBody, MessageType.TOPOLOGY, msg.senderIP, msg.senderPort);
							
							sender.SendMessage(parentEndPoint, JsonSerializer.Serialize(connectionMsg));
							break;
						}
					case MessageType.RESPONSEAPI:
						{
							Console.WriteLine("MessageType: RESPONSEAPI");
							sender.SendMessage(parentEndPoint, message);
							break;
						}
					case MessageType.QUERY:
                        {
							Console.WriteLine("MessageType: QUERY");
							Query q = JsonSerializer.Deserialize<Query>(msg.messageBody);
							if (q == null) throw new Exception("Query is null");

							await scheduler.AddQueryJobAsync(q,parentEndPoint);
							foreach (IPEndPoint child in nodeChildren)
							{
								sender.SendMessage(child, message);
							}
							break;
                        }
					case MessageType.STOP:
                        {
							Console.WriteLine($"MessageType: STOP QUERY {msg.messageBody}");
							try
							{
								var id = JsonSerializer.Deserialize<Guid>(msg.messageBody);
								await scheduler.RemoveQueryjobAsync(id);

								foreach (IPEndPoint child in nodeChildren)
								{ 
									sender.SendMessage(child, message);
								}
							}
							catch (Exception e)
							{
								Console.WriteLine($"Could not parse {msg.messageBody} as GUID");
								Console.WriteLine(e);
							}
							break;
                        }
					case MessageType.TOPOLOGY:
						{
							Console.WriteLine("MessageType: TOPOLOGY");
							sender.SendMessage(parentEndPoint, message);
							break;
						}
					default:
						// TODO: Handle if no type is given
						Log.Warning("No msgType: " + message);
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

