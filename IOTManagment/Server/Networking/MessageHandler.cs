using System;
using System.Net;

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
			// TODO: JSON convert and handle


			// TODO: messageType switchCases/If

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

