// See https://aka.ms/new-console-template for more information
using Services;
using System.Text.Json;

Console.WriteLine("Hello, World!");

//string test = "Select temp, Sum(cpu) Interval 50 Where temp > 50 && (cpu < 40 || temp > 40 || cpu = 50)";
string test = "Select temp, Sum(cpu) Interval 50 Where (temp > 50) && (cpu < 40 || temp > 40 || cpu = 50)";
//string test2 = "(temp > 50 && cpu < 40 || temp > 40 || cpu = 50) ";
//string test = "Select temp, Sum(cpu) Interval 50 Where temp > 50";

QueryParser parser = new QueryParser();
string x = JsonSerializer.Serialize(parser.ParserQuery(test));
Console.WriteLine(JsonSerializer.Serialize(parser.ParserQuery(test)));
