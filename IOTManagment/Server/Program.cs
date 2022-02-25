using NetMQ.Sockets;
using NetMQ;
using Model.Messages;
using Services;
using System.Threading;


//Console.WriteLine("Server starting...");


// ZeroMQ implementation
//using (var server = new ResponseSocket())
//{
//    server.Bind("tcp://*:6000");
//    var communcationService = new CommunicationService();
//    while (true)
//    {
//        // Receive
//        var msg = server.ReceiveFrameString();
//        Message message = new Message(new Guid(), msg);
//        communcationService.ReceiveMessage(message);

//        // Response to sender
//        server.SendFrame("hello");
//    }
//}

//using (var client = new RequestSocket())
//{
//    client.Connect("tcp://127.0.0.1:5556");
//    client.SendFrame("Hello");
//    var msg = client.ReceiveFrameString();
//    Console.WriteLine("From Server: {0}", msg);
//    client.ReceiveFrameString();
//}



var server = new ServerService();
var client = new ClientService();

var thread1 = new Thread(server.BuildServer);
var thread2 = new Thread(client.BuildClient);

thread1.Start();
thread2.Start();


