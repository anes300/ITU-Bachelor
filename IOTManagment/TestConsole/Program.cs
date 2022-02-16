// See https://aka.ms/new-console-template for more information
using NetMQ.Sockets;
using NetMQ;
using TestConsole;


//TestConsole

Console.WriteLine("Setting up socket...");

using (var server = new ResponseSocket())
{
    server.Bind("tcp://*:5556");
    string msg = server.ReceiveFrameString();
    Console.WriteLine("Message from NodeEngine: {0}", msg);

    Console.WriteLine("Enter a message");
    var input = Console.ReadLine();

    if (input != null)
    {
        Communication.Response(input, server);
    }
    else
    {
        Console.WriteLine("No message entered");

    }
}


