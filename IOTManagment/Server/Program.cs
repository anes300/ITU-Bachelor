using NetMQ.Sockets;
using NetMQ;
using Model.Messages;
using Services;
using System.Threading;
using System.Text.Json;
using Server.Networking;
using System.Net;
using Model.Queries;

colorConsole(@"###########################################", ConsoleColor.White,ConsoleColor.Black);
colorConsole(@"#                                         #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"#  ______                        _        #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"# |___  /                       | |       #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"#    / / ___ _ __   ___ ___   __| | ___   #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"#   / / / _ \ '_ \ / __/ _ \ / _` |/ _ \  #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"#  / /_|  __/ | | | (_| (_) | (_| |  __/  #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"# /_____\___|_| |_|\___\___/ \__,_|\___|  #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"#                                         #", ConsoleColor.White, ConsoleColor.Black);
colorConsole(@"###########################################", ConsoleColor.White, ConsoleColor.Black);

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


Console.WriteLine("Available commands:");
Console.WriteLine("stop - Shutdown console-application");
Console.WriteLine("query - ");
Console.WriteLine("nodes - Specific node by id");
Console.WriteLine("clear - Go back to previous 'page'");
Console.WriteLine("Please enter a command");


while (true)
{
    
    string input = Console.ReadLine().ToLower();
    switch (input)
    {
        case "stop"://Shutdown console-application
            Environment.Exit(0);
            break;
        case "query": //Query options
            #region option info
            Console.WriteLine("Select a following option:");
            Console.WriteLine("input - Send query to nodes");
            Console.WriteLine("info - List of all current quries"); 
            Console.WriteLine("stop - Spcific node by id");
            Console.WriteLine("back - Go back to previous 'page'");
            #endregion
            bool isActive = true;
            while (isActive) { 
                string option = Console.ReadLine().ToLower();
                Console.Clear();
                switch (option) 
                {
                    case "input":
                        // Gets query string from console and parses it, and sends it out to child nodes
                        Console.WriteLine("Input Query:");
                        string query = Console.ReadLine();
                        Query q = queryParser.ParserQuery(query);
                        messageHandler.SendQuery(q);
                        break;
                    case "info": //TODO: Create list of all queries
                        break;
                    case "stop": //TODO: send a stop message and remove query from list
                        break;
                    case "-help":
                            Console.WriteLine("Available options:");
                            Console.WriteLine("input - Send query to nodes");
                            Console.WriteLine("info - List of all current quries");
                            Console.WriteLine("stop - Spcific node by id");
                            Console.WriteLine("back - Go back to previous 'page'");
                            break;
                    case "back":
                            isActive = false;
                        break;
                    default:
                            Console.WriteLine("Unknown option. please use '-help' for an overview of options");
                        break;
                }
            }
            break;
        case "nodes":
            //TODO: Showcase all nodes and possibly their info
            //topologyManager.GetIPAdresses();
            break;
        case "clear":
            Console.Clear();
            break;
        case "-help":
            #region help info
            Console.WriteLine("Available commands:");
            Console.WriteLine("stop - Shutdown console-application");
            Console.WriteLine("query - ");
            Console.WriteLine("nodes - Specific node by id");
            Console.WriteLine("clear - Go back to previous 'page'");
            #endregion
            break;
        default:
            Console.Clear();
            colorConsole("Unknown command. Use '-help' to see all commands",ConsoleColor.Red,ConsoleColor.White);
            break;
    }

}

static void colorConsole(string text, ConsoleColor back, ConsoleColor fore)
{
    Console.ForegroundColor = fore;
    Console.BackgroundColor = back;
    Console.WriteLine(text);
    Console.ResetColor(); 
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