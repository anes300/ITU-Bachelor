// See https://aka.ms/new-console-template for more information
using NodeEngine.Networking;
using Services;
using System.Net;
using System.Text.Json;

Console.WriteLine("Hello, World!");

string test = "Select temp, Sum(cpu) Interval 50 Where (temp > 50) && (cpu < 40 || temp > 40 || cpu = 50)";

Console.WriteLine("Enter Connection ip");
var ip = Console.ReadLine();
Console.WriteLine("Enter Connection Port");
int port = int.Parse(Console.ReadLine());

var sender = new NetworkSender(new IPEndPoint(IPAddress.Parse(ip), port), "hej med dig din bølle");
sender.SendMessage();

// Listener
var listener = new NetworkListener();
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6001");

