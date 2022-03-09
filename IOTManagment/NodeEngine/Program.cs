// See https://aka.ms/new-console-template for more information
using Services;
using System.Net;
using System.Text.Json;
using NodeEngine.Networking;
using Model.Messages;

Console.WriteLine("Hello, World!");

string test = "Select temp, Sum(cpu) Interval 50 Where (temp > 50) && (cpu < 40 || temp > 40 || cpu = 50)";

Console.WriteLine("Enter Connection ip");
var ip = Console.ReadLine();
Console.WriteLine("Enter Connection Port");
int port = int.Parse(Console.ReadLine());

// Sender
var msg = new Message(Guid.NewGuid(), "hej fra Node", MessageType.CONNECT, "127.0.0.1", 6001);
var json = JsonSerializer.Serialize(msg);
var sender = new NetworkSender(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000), json);
var senderThread = new Thread(() => sender.SendMessage());
senderThread.Start();

// Listener
var listener = new NetworkListener();
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6001");

