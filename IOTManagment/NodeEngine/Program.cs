// See https://aka.ms/new-console-template for more information
using NodeEngine.Services;
using Services;
using System.Net;
using System.Text.Json;
using NodeEngine.Networking;
using Model.Messages;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");
SensorManager sensorManager = new SensorManager();
//string test = "Select temp, Sum(cpu) Interval 50 Where (temp > 50) && (cpu < 40 || temp > 40 || cpu = 50)";

//QueryParser parser = new QueryParser();
//string x = JsonSerializer.Serialize(parser.ParserQuery(test));
//Console.WriteLine(JsonSerializer.Serialize(parser.ParserQuery(test)));

// Setup Receiver for CONNECT Message
Console.WriteLine("Enter Connection ip");
var recieverIp = Console.ReadLine();
Console.WriteLine("Enter Connection Port");
int recieverPort = int.Parse(Console.ReadLine());

// Listener (OBS: LISTNER SHOULD RUN FIRST - CAN'T SEND WITHOUT LISTENER)
var listener = new NetworkListener();
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6001");

// Get Local IP Address
string nodeIp = default;

var host = Dns.GetHostEntry(Dns.GetHostName());
foreach (var localIp in host.AddressList)
{
    if (localIp.AddressFamily == AddressFamily.InterNetwork && localIp.ToString() != "127.0.0.1")
    {
        nodeIp = localIp.ToString();
        Console.WriteLine("IP Address of this node = " + nodeIp);
    }
}

// Sender
var msg = new Message(Guid.NewGuid(), "hej fra Node", MessageType.CONNECT, nodeIp, 6001);
var json = JsonSerializer.Serialize(msg);
var sender = new NetworkSender(new IPEndPoint(IPAddress.Parse(recieverIp), recieverPort), json);
var senderThread = new Thread(() => sender.SendMessage());
senderThread.Start();