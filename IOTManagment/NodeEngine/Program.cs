// See https://aka.ms/new-console-template for more information
using Services;

Console.WriteLine("Hello, World!");

string test = "Select temp, Sum(cpu) Interval 50 Where temp > 50";

QueryParser parser = new QueryParser();

Console.WriteLine(parser.ParserQuery(test));
