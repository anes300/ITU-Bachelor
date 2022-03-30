// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text.Json;
using Serilog;
using Microsoft.Extensions.Logging;
using NodeEngine.Networking;
using Model.Messages;
using System.Net.Sockets;
using Model.Nodes;
using Model.Nodes.Enum;
using NodeEngine.Utils;

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

// Setup Receiver for CONNECT Message
Console.WriteLine("Enter Connection IP");
var recieverIp = Console.ReadLine();
Console.WriteLine("Enter Connection Port");
int recieverPort = int.Parse(Console.ReadLine());

var reciever = new IPEndPoint(IPAddress.Parse(recieverIp), recieverPort);
MessageHandler handler = new MessageHandler(reciever);

// Listener (OBS: LISTNER SHOULD RUN FIRST - CAN'T SEND WITHOUT LISTENER)
var listener = new NetworkListener(handler);
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6001");

// Get Local IP Address
string nodeIp = IpUtils.GetLocalIp();
Console.WriteLine($"IP Address of this Node: {nodeIp}");

//EndPoint
var NodeEndPoint = new IPEndPoint(IPAddress.Parse(nodeIp), 6001);

//Node & serialization
var node = new Node
{
    Parent = recieverIp,
    ParentPort = recieverPort,
    Address = nodeIp,
    AddressPort = 6001,
    Type = NodeType.NODE,
    Status = Status.ACTIVE,
    DataType = DataType.TEMPERATURE_CPU,
};

var jsonNode = JsonSerializer.Serialize(node);

// Sender-connect message to server.
var msg = new Message(jsonNode, MessageType.CONNECT, nodeIp, 6001);
var json = JsonSerializer.Serialize(msg);
var sender = new NetworkSender(reciever, json);
var senderThread = new Thread(() => sender.SendMessage());
senderThread.Start();