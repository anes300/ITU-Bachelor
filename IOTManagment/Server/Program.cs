using NetMQ.Sockets;
using NetMQ;
using Model.Messages;
using Services;
using System.Threading;
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
var sender = new NetworkSender(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000), "hej");
var senderThread = new Thread(() => sender.SendMessage());
senderThread.Start();

// Sender2
var sender2 = new NetworkSender(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000), "hej2");
var senderThread2 = new Thread(() => sender2.SendMessage());
senderThread2.Start();

// Sender3
var sender3 = new NetworkSender(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000), "hej3");
var senderThread3 = new Thread(() => sender3.SendMessage());
senderThread3.Start();

