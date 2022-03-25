using System;
using System.Net;
using System.Text.Json;
using Model.Messages;
using Model.Queries;
using NodeEngine.Services;

namespace NodeEngine.Networking
{
	public class MessageHandler
	{
		List<IPEndPoint> nodeChildren;
		QueryScheduler scheduler;
		IPEndPoint parentEndPoint;
		public MessageHandler(IPEndPoint parent)
		{
			nodeChildren = new List<IPEndPoint>();
			scheduler = new QueryScheduler();
			parentEndPoint = parent;
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
							break;
						}
					case MessageType.FORWARD:
						{
							Console.WriteLine("MessageType: FOWARD");
							
							foreach (IPEndPoint child in nodeChildren)
							{
								var sender = new NetworkSender(child, message);
								sender.SendMessage();
							}

							//TODO: Do something to Query

							break;
						}
					case MessageType.RESPONSEAPI:
						{
							Console.WriteLine("MessageType: RESPONSEAPI");
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
								var sender = new NetworkSender(child, message);
								sender.SendMessage();
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
									var sender = new NetworkSender(child, message);
									sender.SendMessage();
								}
							}
							catch (Exception e)
							{
								Console.WriteLine($"Could not parse {msg.messageBody} as GUID");
								Console.WriteLine(e);
							}
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

