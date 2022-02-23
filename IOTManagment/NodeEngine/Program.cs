// See https://aka.ms/new-console-template for more information
using Services;
using System.Net;
using Model.Nodes;
using Model.Enum;

Console.WriteLine("Hello, World!");

TopologyManager Manager = new TopologyManager();
IPAddress rootAdd = IPAddress.Parse("127.0.0.1");
IPAddress rNodeAdd = IPAddress.Parse("127.0.0.2");
IPAddress nodeAdd = IPAddress.Parse("127.0.0.3");
Node Root = new Node(rootAdd, (Status) 1, (NodeType) 4);
Node RouteNode = new Node(rNodeAdd, rootAdd, (Status)1, (NodeType)2, (DataType)1);
Node Node = new Node(nodeAdd, rNodeAdd, (Status)1, (NodeType)2, (DataType)1);


Console.WriteLine(Manager.IPAdresses.Count);
Manager.AddNode(rootAdd, Root);
Console.WriteLine(Manager.IPAdresses.Count);
Manager.AddNode(rNodeAdd, RouteNode);
Console.WriteLine(Manager.IPAdresses.Count);
var newNode = new Node (rNodeAdd,Manager.IPAdresses.GetValueOrDefault(rNodeAdd).Parent,(Status)1,(NodeType)3,(DataType)1);

Manager.IPAdresses.GetValueOrDefault(rNodeAdd);

Manager.UpdateNode(rNodeAdd, newNode);

Manager.AddNode(nodeAdd, Node);
Console.WriteLine(Manager.IPAdresses.Count);


Console.WriteLine(Manager.IPAdresses.GetValueOrDefault(rootAdd));
Manager.IPAdresses.GetValueOrDefault(rNodeAdd);
Manager.IPAdresses.GetValueOrDefault(nodeAdd);