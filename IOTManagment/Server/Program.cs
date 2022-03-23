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



Console.WriteLine("Server starting...");

TopologyManager topologyManager = new();

MessageHandler messageHandler = new(topologyManager);
QueryParser queryParser = new QueryParser();

// Listener
var listener = new NetworkListener(messageHandler);
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6000");



Console.Clear();
while (true)
{
    printCommands(0);
    string input = Console.ReadLine().ToLower();
    bool isActive = true;
    switch (input)
    {
        case "stop"://Shutdown console-application
            Environment.Exit(0);
            break;
        case "query": //Query options
            Console.Clear();
            while (isActive) { 
            printCommands(1);
                string options = Console.ReadLine().ToLower().Trim();
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
                        printCommands(1);
                            break;
                    case "back":
                        Console.Clear();
                            isActive = false;
                        break;
                    default:
                        Console.Clear();
                        colorConsole("Unknown option. Use '-help' for an overview of options", ConsoleColor.Red, ConsoleColor.White);
                        break;
                }
            }
            break;
        case "nodes":
            Console.Clear();

            while (isActive)
            {
            printCommands(2);
                string option = Console.ReadLine().ToLower().Trim();
                switch (option)
                {
                    case "all":
                        var listofnodes = topologyManager.GetIPAdresses().Values.ToList();
                        int counter = 1;
                        Console.Clear();
                        Console.WriteLine("              Active Nodes              ");
                        Console.WriteLine("----------------------------------------");

                        if (listofnodes.Count() == 0)
                        {
                            colorConsole("No Active nodes...", ConsoleColor.Red, ConsoleColor.White);
                            Console.WriteLine("\n");
                            
                            break;
                        }
                        foreach (var tmp in listofnodes)
                        {
                            Console.WriteLine($"{counter}| NodeIP:{tmp.Address}:{tmp.AddressPort}");
                            Console.WriteLine("----------------------------------------");
                            counter++;
                        }
                        Console.WriteLine("\n");
                        break;

                    case "node":
                        Console.WriteLine("Enter IPEndPoint:");
                        bool innerIsActive = true;

                        string parseIp = default;
                        IPEndPoint ip = default(IPEndPoint);
                        while (innerIsActive)
                        {
                            parseIp = Console.ReadLine().ToLower().Trim();
                            if (parseIp == "quit") { Console.Clear(); break; }
                            if (parseIp != "")
                            {
                                try
                                {
                                    IPEndPoint.TryParse(parseIp, out ip);
                                    var node = topologyManager.GetNodeByIP(ip);
                                    if (node == null)
                                    {
                                        Console.WriteLine($"No such IPEndPoint exists: {ip}");
                                        Console.WriteLine("Re-enter IPEndPoint or write quit");
                                    }
                                    else
                                    {
                                        #region META data printed
                                        Console.Clear();

                                        Console.WriteLine($"NodeIP:{node.Address}:{node.AddressPort}");
                                        Console.WriteLine("----------------------------------------");
                                        Console.WriteLine($"NodeIP:{node.Parent}:{node.ParentPort}");
                                        Console.WriteLine($"Parent:{node.Parent}:{node.ParentPort}");
                                        Console.WriteLine($"Type:{node.Type}:");
                                        Console.WriteLine($"Status:{node.Status}");
                                        Console.WriteLine($"DataType:{node.DataType}");
                                        #endregion
                                    }
                                }
                                catch (Exception ex)
                                {
                                    colorConsole("Incorrect format: Use IP formating", ConsoleColor.Red, ConsoleColor.White);
                                    Console.WriteLine("Re-enter IPEndPoint or write quit");
                                }
                                
                            }else {
                                Console.WriteLine("Re-enter IPEndPoint or write quit");
                            }
                        }
                        break;
                    case "back":
                        Console.Clear();
                        isActive = false;
                        break;
                    case "-help":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        colorConsole("Unknown option. Use '-help' to see all options", ConsoleColor.Red, ConsoleColor.White);
                        break;
                }   
            }
            break;
        case "clear":
            Console.Clear();
            break;
        case "-help":
            Console.Clear();
            break;
        case "logo":
            Console.Clear();
            printCommands(3);
            break;
        default:
            Console.Clear();
            colorConsole("Unknown command. Use '-help' to see all commands",ConsoleColor.Red,ConsoleColor.White);
            break;
    }
}

static void printCommands(int index)
{
    switch(index)
    {
        case 0: //Main
            #region Main Printstatement
            colorConsole("__________MAIN___________", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsole("Available commands:      ", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsoleSame("stop ", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Shutdown console-application.");
            colorConsoleSame("query", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Query interface.");
            colorConsoleSame("nodes", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Node interface.");
            colorConsoleSame("clear", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Clears the current Console text.");
            Console.WriteLine("Please enter a command:");
            #endregion
            break;
        case 1: //Query
            #region Query Printstatement
            colorConsole("__________QUERY___________", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsole("Available options:        ", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsoleSame("input", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Send query to nodes");
            colorConsoleSame("info ", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - List of all current quries");
            colorConsoleSame("stop ", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Specific node by id");
            colorConsoleSame("back ", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Go back to previous 'page'");
            #endregion
            break;

        case 2://Node
            #region Node Printstatement
            colorConsole("___________NODE___________", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsole("Select a following option:", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsoleSame("all ", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Displays all current nodes");
            colorConsoleSame("node", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Displays information about a specific node via IPEndPoint");
            colorConsoleSame("back", ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(" - Go back to previous 'page'");
            #endregion
            break;
        case 3://Funny
            #region logo
            colorConsole(@"###########################################", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#  ______                        _        #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"# |___  /                       | |       #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#    / / ___ _ __   ___ ___   __| | ___   #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#   / / / _ \ '_ \ / __/ _ \ / _` |/ _ \  #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#  / /_|  __/ | | | (_| (_) | (_| |  __/  #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"# /_____\___|_| |_|\___\___/ \__,_|\___|  #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#                                         #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"###########################################", ConsoleColor.White, ConsoleColor.Black);
            #endregion
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