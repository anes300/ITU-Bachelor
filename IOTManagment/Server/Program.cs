using NetMQ.Sockets;
using NetMQ;
using Model.Messages;
using Services;
using System.Threading;
using System.Text.Json;
using Server.Networking;
using System.Net;
using Model.Queries;
using Model.Nodes;

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



while (true)
{
    #region Commmand info
    colorConsole("Available commands:", ConsoleColor.DarkGreen, ConsoleColor.White);
    colorConsoleSame("stop", ConsoleColor.White, ConsoleColor.Black);
    Console.WriteLine(" - Shutdown console-application");
    colorConsoleSame("query", ConsoleColor.White, ConsoleColor.Black);
    Console.WriteLine(" - ");
    colorConsoleSame("nodes", ConsoleColor.White, ConsoleColor.Black);
    Console.WriteLine(" - Specific node by id");
    colorConsoleSame("clear", ConsoleColor.White, ConsoleColor.Black);
    Console.WriteLine(" - Go back to previous 'page'");
    Console.WriteLine("Please enter a command:");
    #endregion Command info

    string input = Console.ReadLine().ToLower();
    bool isActive = true;
    switch (input)
    {
        case "stop"://Shutdown console-application
            Environment.Exit(0);
            break;
        case "query": //Query options
            #region Query option info
            Console.WriteLine("Select a following option:");
            Console.WriteLine("input - Send query to nodes");
            Console.WriteLine("info - List of all current quries"); 
            Console.WriteLine("stop - Specific node by id");
            Console.WriteLine("back - Go back to previous 'page'");
            #endregion
            while (isActive) { 
                string options = Console.ReadLine().ToLower();
                Console.Clear();
                switch (options) 
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
                            Console.WriteLine("stop - Specific node by id");
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
            Console.Clear();
            #region Node option info
            colorConsole("___________NODE___________", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsole("Select a following option:", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsoleSame("all", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Displays all current nodes");
            colorConsoleSame("node", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Displays information about a specific node via IPEndPoint");
            colorConsoleSame("back", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Go back to previous 'page'");
            #endregion

            while (isActive)
            {
                string option = Console.ReadLine().ToLower().Trim();
                switch (option)
                {
                    case "all":
                        int counter = 1;
                        Console.Clear();
                        Console.WriteLine("              Active Nodes              ");
                        Console.WriteLine("----------------------------------------");

                        foreach (var tmp in topologyManager.GetIPAdresses().Values.ToList())
                        {
                            Console.WriteLine($"{counter}| NodeIP:{tmp.Address}:{tmp.AddressPort}");
                            Console.WriteLine("----------------------------------------");
                            counter++;
                        }
                        break;
                    case "node":
                        Console.WriteLine("Enter IPEndPoint:");
                        bool innerIsActive = true;

                        string parseIp = default;
                        while (innerIsActive)
                        {
                            parseIp = Console.ReadLine().ToLower().Trim();
                            if (parseIp != "") break;
                            Console.WriteLine("Re-enter IPEndPoint");
                        }
                        IPEndPoint ip = default(IPEndPoint);

                        IPEndPoint.TryParse(parseIp, out ip);

                        var node = topologyManager.GetNodeByIP(ip);
                        if (node == null) { Console.WriteLine($"Not such IPEndPoint exists: {ip}"); break; }
                        #region META data printed
                        Console.Clear();
                        Console.WriteLine($"NodeIP:{node.Address}:{node.AddressPort}");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine($"Parent:{node.Parent}:{node.ParentPort}");
                        Console.WriteLine($"Type:{node.Type}:");
                        Console.WriteLine($"Status:{node.Status}");
                        Console.WriteLine($"DataType:{node.DataType}");
                        #endregion
                        break;

                    case "back":
                        Console.Clear();
                        isActive = false;
                        break;
                    case "-help":
                        #region Node option info
                        colorConsole("Select a following option:", ConsoleColor.DarkGreen, ConsoleColor.White);
                        colorConsoleSame("all", ConsoleColor.White, ConsoleColor.Black);
                        Console.WriteLine(" - Displays all current nodes");
                        colorConsoleSame("node", ConsoleColor.White, ConsoleColor.Black);
                        Console.WriteLine(" - Displays information about a specific node via IPEndPoint");
                        colorConsoleSame("back", ConsoleColor.White, ConsoleColor.Black);
                        Console.WriteLine(" - Go back to previous 'page'");
                        #endregion
                        break;
                    default:
                        Console.Clear();
                        colorConsole("Unknown command. Use '-help' to see all commands", ConsoleColor.Red, ConsoleColor.White);
                        break;
                }
                break;
            }
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

static void colorConsoleSame(string text, ConsoleColor back, ConsoleColor fore)
{
    Console.ForegroundColor = fore;
    Console.BackgroundColor = back;
    Console.Write(text);
    Console.ResetColor();
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