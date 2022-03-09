// See https://aka.ms/new-console-template for more information
using NodeEngine.Services;
using Services;
using System.Text.Json;
using NodeEngine.Jobs;
using Model.Queries;
using Model.Queries.Statements;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;


// Setup logger
var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();
Log.Logger = log;

// create log factory for scheduler
var logFactory = new LoggerFactory()
    .AddSerilog(log);
// sets the logger for the Scheduler 
Quartz.Logging.LogContext.SetCurrentLogProvider(logFactory);
QueryScheduler scheduler = new QueryScheduler();
Console.WriteLine("Query Engine Started");

string test = "Select temp, Sum(cpu) Interval 1000 Where (temp > 50) && (cpu < 40 || temp > 40 || cpu = 50)";

string test2 = "Select CPU, Sum(cpu) Interval 1000 Where (temp > 50) && (cpu < 40 || temp > 40 || cpu = 50)";


log.Information("");

QueryParser parser = new QueryParser();
Console.WriteLine(JsonSerializer.Serialize(parser.ParserQuery(test)));
var query = parser.ParserQuery(test);
var query2 = parser.ParserQuery(test2);

await scheduler.AddQueryJobAsync(query2);
await scheduler.AddQueryJobAsync(query);

await Task.Delay(TimeSpan.FromSeconds(10));
Console.WriteLine("Removing query 2");
await scheduler.RemoveQueryjobAsync(query2.Id);
await Task.Delay(TimeSpan.FromSeconds(10));

