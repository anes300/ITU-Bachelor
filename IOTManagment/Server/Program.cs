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
List<Query> queries = new List<Query>();
// Listener
var listener = new NetworkListener(messageHandler);
var listenerThread = new Thread(() => listener.StartListener());
listenerThread.Start();
Console.WriteLine("Started Listener on port 6000");
string input = Console.ReadLine().ToLower().Trim();
string query = "Select TEST_VAR Interval 5000 Where (TEST_VAR > 30)";
for (int i = 0; i < 200; i++)
{
    Query q = queryParser.ParserQuery(query);
    queries.Add(q);
    messageHandler.SendQuery(q);
    Thread.Sleep(200);
}

//Console.Clear();
//while (true)
//{
//    printCommands(0);
//    string input = Console.ReadLine().ToLower().Trim();
//    bool isActive = true;
    
//    switch (input)
//    {
//        case "stop"://Shutdown console-application
//            Environment.Exit(0);
//            break;
//        case "query":
//            //Query options
//            Console.Clear();
//            while (isActive) { 
//            printCommands(1);
//                string options = Console.ReadLine().ToLower().Trim();
//                switch (options) 
//                {
//                    case "input":
//                        // Gets query string from console and parses it, and sends it out to child nodes
//                        bool innerIsActive = true;
//                        Console.WriteLine("Input Query:");
//                        while (innerIsActive)
//                        {
//                            string query = Console.ReadLine().Trim();
//                            if (query == "quit") { Console.Clear(); break; }
//                            if (query != "")
//                            {
//                                try
//                                {
//                                    Query q = queryParser.ParserQuery(query);
//                                    queries.Add(q);
//                                    messageHandler.SendQuery(q);
//                                    Console.Clear();
//                                    colorConsole("Query sent", ConsoleColor.Yellow, ConsoleColor.Black);
//                                    break;
//                                }
//                                catch (Exception ex)
//                                {
//                                    colorConsole("Incorrect format: Use Query formating", ConsoleColor.Red, ConsoleColor.White);
//                                    Console.WriteLine("Re-enter query or write quit");
//                                }
//                            }
//                            else
//                            {
//                                Console.WriteLine("Re-enter Query or write quit");
//                            }
//                        }
//                        break;
//                    case "info": //TODO: Create list of all queries
//                        Console.Clear();
//                        int counter = 1;
//                        Console.Clear();
//                        Console.WriteLine("              Active Quries              ");
//                        Console.WriteLine("----------------------------------------");

//                        if (queries.Count() == 0)
//                        {
//                            colorConsole("No Active quries...", ConsoleColor.Red, ConsoleColor.White);
//                            Console.WriteLine("");
//                            break;
//                        }
//                        foreach (Query x in queries)
//                        {
//                            Console.WriteLine($"{counter}| QueryId: {x.Id}");
//                            Console.WriteLine("----------------------------------------");
//                            counter++;
//                        }
//                        break;
//                    case "stop": //TODO: send a stop message and remove query from list
//                        Console.WriteLine("Input query id");
//                        Guid id = default;
//                        while (true)
//                        {
//                            string value = Console.ReadLine().Trim();
//                            if (value == "quit") { Console.Clear(); break; }
//                            if (value != "")
//                            {
//                                Guid.TryParse(value, out id);
//                                var foundQ = queries.Where(x => x.Id == id).FirstOrDefault();
//                                if (foundQ != null)
//                                {
//                                    Console.Clear();
//                                    queries.Remove(foundQ);
//                                    colorConsole($"Query removed {id}", ConsoleColor.Yellow, ConsoleColor.Black);
//                                    messageHandler.SendStop(id);
//                                    break;
//                                }
//                                else { colorConsole("No such Query found", ConsoleColor.Red, ConsoleColor.White); Console.WriteLine("Please re-enter query id or write quit"); }
//                            }
//                            else
//                            {
//                                Console.WriteLine("Re-enter query id or write quit");
//                            }
//                        }
//                        break;
//                    case "-help":
//                        Console.Clear();
//                            break;
//                    case "back":
//                        Console.Clear();
//                            isActive = false;
//                        break;
//                    default:
//                        Console.Clear();
//                        colorConsole("Unknown option. Use '-help' for an overview of options", ConsoleColor.Red, ConsoleColor.White);
//                        break;
//                }
//            }
//            break;
//        case "nodes":
//            Console.Clear();
//            while (isActive)
//            {
//            printCommands(2);
//                string option = Console.ReadLine().ToLower().Trim();
//                switch (option)
//                {
//                    case "all":
//                        var listofnodes = topologyManager.GetIPAdresses().Values.ToList();
//                        int counter = 1;
//                        Console.Clear();
//                        Console.WriteLine("              Active Nodes              ");
//                        Console.WriteLine("----------------------------------------");

//                        if (listofnodes.Count() == 0)
//                        {
//                            colorConsole("No Active nodes...", ConsoleColor.Red, ConsoleColor.White);
//                            Console.WriteLine("");
                            
//                            break;
//                        }
//                        foreach (var tmp in listofnodes)
//                        {
//                            Console.WriteLine($"{counter}| NodeIP: {tmp.Address}:{tmp.AddressPort}");
//                            Console.WriteLine("----------------------------------------");
//                            counter++;
//                        }
//                        Console.WriteLine("");
//                        break;

//                    case "node":
//                        Console.WriteLine("Enter IPEndPoint:");
//                        bool innerIsActive = true;

//                        string parseIp = default;
//                        IPEndPoint ip = default(IPEndPoint);
//                        while (innerIsActive)
//                        {
//                            parseIp = Console.ReadLine().ToLower().Trim();
//                            if (parseIp == "quit") { Console.Clear(); break; }
//                            if (parseIp != "")
//                            {
//                                try
//                                {
//                                    IPEndPoint.TryParse(parseIp, out ip);
//                                    var node = topologyManager.GetNodeByIP(ip);
//                                    if (node != null)
//                                    {
//                                        #region META data printed
//                                        Console.Clear();
//                                        colorConsole("------------------INFO-------------------",ConsoleColor.Yellow,ConsoleColor.Black);
//                                        Console.WriteLine($"NodeIP: {node.Address}:{node.AddressPort}");
//                                        Console.WriteLine("----------------------------------------");
//                                        Console.WriteLine($"Parent: {node.Parent}:{node.ParentPort}");
//                                        Console.WriteLine($"Type: {node.Type}");
//                                        Console.WriteLine($"Status: {node.Status}");
//                                        Console.WriteLine($"DataType: {node.DataType}");
//                                        Console.WriteLine("");
//                                        #endregion
//                                        break;
//                                    }
//                                    else
//                                    {
//                                        Console.WriteLine($"No such IPEndPoint exists: {ip}");
//                                        Console.WriteLine("Re-enter IPEndPoint or write quit");
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    colorConsole("Incorrect format: Use IP formating", ConsoleColor.Red, ConsoleColor.White);
//                                    Console.WriteLine("Re-enter IPEndPoint or write quit");
//                                }
//                            }else {
//                                Console.WriteLine("Re-enter IPEndPoint or write quit");
//                            }
//                        }
//                        break;
//                    case "back":
//                        Console.Clear();
//                        isActive = false;
//                        break;
//                    case "-help":
//                        Console.Clear();
//                        break;
//                    default:
//                        Console.Clear();
//                        colorConsole("Unknown option. Use '-help' to see all options", ConsoleColor.Red, ConsoleColor.White);
//                        break;
//                }   
//            }
//            break;
//        case "clear":
//            Console.Clear();
//            break;
//        case "-help":
//            Console.Clear();
//            break;
//        case "logo":
//            Console.Clear();
//            printCommands(3);
//            break;
//        default:
//            Console.Clear();
//            colorConsole("Unknown command. Use '-help' to see all commands",ConsoleColor.Red,ConsoleColor.White);
//            break;
//    }
//}

static void printCommands(int index)
{
    switch(index)
    {
        case 0: //Main
            #region Main Printstatement
            colorConsole("__________MAIN___________", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsole("Available commands:      ", ConsoleColor.DarkGreen, ConsoleColor.White);
            colorConsoleSame("stop ", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Shutdown console-application.");
            colorConsoleSame("query", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Query interface.");
            colorConsoleSame("nodes", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Node interface.");
            colorConsoleSame("clear", ConsoleColor.White, ConsoleColor.Blue);
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
            colorConsoleSame("all ", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Displays all current nodes");
            colorConsoleSame("node", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Displays information about a specific node via IPEndPoint");
            colorConsoleSame("back", ConsoleColor.White, ConsoleColor.Blue);
            Console.WriteLine(" - Go back to previous 'page'");
            #endregion
            break;
        case 3://Funny
            #region logo
            //Normal
            /* 
            colorConsole(@"###########################################", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#  ______                        _        #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"# |___  /                       | |       #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#    / / ___ _ __   ___ ___   __| | ___   #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#   / / / _ \ '_ \ / __/ _ \ / _` |/ _ \  #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#  / /_|  __/ | | | (_| (_) | (_| |  __/  #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"# /_____\___|_| |_|\___\___/ \__,_|\___|  #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"#                                         #", ConsoleColor.White, ConsoleColor.Black);
            colorConsole(@"###########################################", ConsoleColor.White, ConsoleColor.Black);
            */

            //Ukraine-flag
            
            colorConsole(@"###########################################", ConsoleColor.Blue, ConsoleColor.White);
            colorConsole(@"#  ______                        _        #", ConsoleColor.Blue, ConsoleColor.White);
            colorConsole(@"# |___  /                       | |       #", ConsoleColor.Blue, ConsoleColor.White);
            colorConsole(@"#    / / ___ _ __   ___ ___   __| | ___   #", ConsoleColor.Blue, ConsoleColor.White);
            colorConsole(@"#   / / / _ \ '_ \ / __/ _ \ / _` |/ _ \  #", ConsoleColor.Blue, ConsoleColor.White);
            colorConsole(@"#  / /_|  __/ | | | (_| (_) | (_| |  __/  #", ConsoleColor.Yellow, ConsoleColor.Black);
            colorConsole(@"# /_____\___|_| |_|\___\___/ \__,_|\___|  #", ConsoleColor.Yellow, ConsoleColor.Black);
            colorConsole(@"#                                         #", ConsoleColor.Yellow, ConsoleColor.Black);
            colorConsole(@"###########################################", ConsoleColor.Yellow, ConsoleColor.Black);
            

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