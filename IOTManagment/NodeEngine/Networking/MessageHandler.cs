using Model.Messages;
using System;
using System.Net;
using System.Text.Json;


namespace NodeEngine.Networking
{
	public class MessageHandler
	{
		List<IPEndPoint> ChildrenNode;
		public MessageHandler()
		{
		ChildrenNode = new List<IPEndPoint>();
		}

		public void HandleMessage(string message)
        {
			var msg = JsonSerializer.Deserialize<Message>(message);
			// TODO: Convert JSON to object
			// TODO: MessageType
			// TODO: Handle If statements
			switch (msg.messageType) 
			{
				case MessageType.CONNECT:
					//When connecting a new node, add this to a list of children.
					ChildrenNode.Add(new IPEndPoint(IPAddress.Parse(msg.senderIP),msg.senderPort));
					break;

				case MessageType.FORWARD:
					//Forwarding msg-object to all node's children.
					var json = JsonSerializer.Serialize(msg);
					foreach (var Child in ChildrenNode) {
						var sender = new NetworkSender(Child, json);
						sender.SendMessage();
					}
					break;
				default:
					Console.WriteLine("Anes burde gøre noget her :)");
					break;
			}
			// If MessageType == Query
				// Handle Query
				// Send Query to Child

			// if MessageType = ConnectMessage
				// Check for Parent - if Parent for new Node, register new child to list
				// Send to Parent => (Server / Topology Manager)
        }
	}
}

