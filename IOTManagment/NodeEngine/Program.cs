// See https://aka.ms/new-console-template for more information
using NodeEngine.Services;
using Services;
using System.Net;
using System.Text.Json;
using NodeEngine.Jobs;
using Model.Queries;
using Model.Queries.Statements;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NodeEngine.Networking;
using Model.Messages;
using System.Net.Sockets;
using Model.Nodes;
using Model.Nodes.Enum;

// Setup logger
var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();
// Set global logger
Log.Logger = log;
// create log factory for scheduler
var logFactory = new LoggerFactory()
    .AddSerilog(log);
// sets the logger for the Scheduler 
Quartz.Logging.LogContext.SetCurrentLogProvider(logFactory);

Console.WriteLine("Enter Nodes port");
int port = int.Parse(Console.ReadLine());

// Setup Receiver for CONNECT Message
Console.WriteLine("Enter Receiver Connection ip");
var recieverIp = Console.ReadLine();
Console.WriteLine("Enter Receiver Connection Port");
int recieverPort = int.Parse(Console.ReadLine());

var reciever = new IPEndPoint(IPAddress.Parse(recieverIp), recieverPort);
MessageHandler handler = new MessageHandler(reciever);

// Listener (OBS: LISTNER SHOULD RUN FIRST - CAN'T SEND WITHOUT LISTENER)
var listener = new NetworkListener(handler);
var listenerThread = new Thread(() => listener.StartListener(port));
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

//EndPoint
var NodeEndPoint = new IPEndPoint(IPAddress.Parse(nodeIp), port);

//Node & serialization
var node = new Node
{
    Parent = recieverIp,
    ParentPort = recieverPort,
    Address = nodeIp,
    AddressPort = port,
    Type = NodeType.NODE,
    Status = Status.ACTIVE,
    DataType = DataType.TEMPERATURE_CPU,
};

var jsonNode = JsonSerializer.Serialize(node);

// Sender-connect message to server.
var msg = new Message(jsonNode, MessageType.CONNECT, nodeIp, port);
var json = JsonSerializer.Serialize(msg);
var sender = new NetworkSender(reciever, json);
var senderThread = new Thread(() => sender.SendMessage());
senderThread.Start();