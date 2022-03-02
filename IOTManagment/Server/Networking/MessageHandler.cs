using System;
namespace Server.Networking
{
	public class MessageHandler
	{
		public MessageHandler()
		{
		}

		public static void HandleMessage(string message)
        {
			// TODO: JSON convert and handle
			// TODO: messageType
			// TODO: Handle if statements

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

