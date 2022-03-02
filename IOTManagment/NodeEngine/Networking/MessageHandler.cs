using System;
namespace NodeEngine.Networking
{
	public class MessageHandler
	{
		public MessageHandler()
		{
		}

		public static void HandleMessage(string message)
        {
			// TODO: Convert JSON to object
			// TODO: MessageType
			// TODO: Handle If statements


			// If MessageType == Query
				// Handle Query
				// Send Query to Child

			// if MessageType = ConnectMessage
				// Check for Parent - if Parent for new Node, register new child to list
				// Send to Parent => (Server / Topology Manager)
        }
	}
}

