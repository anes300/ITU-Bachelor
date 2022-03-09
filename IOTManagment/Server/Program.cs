using NetMQ.Sockets;
using NetMQ;
using Model.Messages;
using Services;
using System.Threading;
using System.Text.Json;
using Server.Networking;
using System.Net;

Console.WriteLine("Server starting...");

// TODO: Make threads for NetworkListener

// Listener
var listener = new NetworkListener();
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6000");

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