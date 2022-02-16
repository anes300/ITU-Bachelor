// See https://aka.ms/new-console-template for more information
using NetMQ.Sockets;
using NetMQ;
using NodeEngine;


//NodeEngine

Console.WriteLine("Connecting to socket...");

Console.WriteLine("Enter a message");
var input = Console.ReadLine();


using (var client = new RequestSocket())
{
    client.Connect("tcp://127.0.0.1:5556");
    Communication.Request(input, client);
    
}