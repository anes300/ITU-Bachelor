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
QueryScheduler scheduler = new QueryScheduler();
Console.WriteLine("Query Engine Started");

string test = "Select temp, Sum(cpu) Interval 1000 Where (temp > 50)";

string test2 = "Select CPU, Sum(cpu) Interval 1000 Where (temp > 50) && (cpu < 40 || temp > 40 && cpu = 50)";

QueryParser parser = new QueryParser();
Console.WriteLine(JsonSerializer.Serialize(parser.ParserQuery(test)));
var query = parser.ParserQuery(test);
var query2 = parser.ParserQuery(test2);

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