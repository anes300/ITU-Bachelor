using NetMQ.Sockets;
using NetMQ;
using Model.Messages;
using Services;
using System.Threading;
using System.Text.Json;
using Server.Networking;
using System.Net;
using Model.Queries;

Console.WriteLine("Server starting...");

TopologyManager topologyManager = new();

MessageHandler messageHandler = new(topologyManager);
QueryParser queryParser = new QueryParser();
// TODO: Make threads for NetworkListener

// Listener
var listener = new NetworkListener(messageHandler);
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6000");

bool isActive = true;
while(isActive)
{
    string input = Console.ReadLine();

    switch (input)
    {
        case "query":
            // Gets query string from console and parses it, and sends it out to child nodes
            Console.WriteLine("Input Query:");
            string query = Console.ReadLine();
            Query q = queryParser.ParserQuery(query);
            messageHandler.SendQuery(q);
            break;
        case "stop":
            isActive = false;
            break;
        default:
            break;
    }
}




// Sender
/*var msg = new Message(Guid.NewGuid(), "hej fra server", MessageType.CONNECT, "127.0.0.1", 6000);
var json = JsonSerializer.Serialize(msg);
var sender = new NetworkSender(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000), json);
var senderThread = new Thread(() => sender.SendMessage());
senderThread.Start();

var msg2 = new Message(Guid.NewGuid(), "hej fra server 2", MessageType.CONNECT, "127.0.0.1", 6001);
var json2 = JsonSerializer.Serialize(msg2);
var sender2 = new NetworkSender(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6001), json2);
var senderThread2 = new Thread(() => sender2.SendMessage());
senderThread2.Start();*/