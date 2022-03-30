using Model.Messages;
using Model.Queries.Statements;
using NodeEngine.Networking;
using NodeEngine.Services;
using Quartz;
using Serilog;
using System.Net;
using System.Text.Json;
using NodeEngine.Utils;

namespace NodeEngine.Jobs
{
    public class QueryExecutionJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            SensorManager sensorManager = new SensorManager();
            QueryHandler handler = new QueryHandler(sensorManager);

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            SelectStatement? selectStatement = JsonSerializer.Deserialize<SelectStatement>(dataMap.GetString("Select"));
            WhereStatement? whereStatement = JsonSerializer.Deserialize<WhereStatement>(dataMap.GetString("Where"));
            
            if(whereStatement != null && handler.CheckWhereStatement(whereStatement))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(dataMap.GetString("IP")), int.Parse(dataMap.GetString("Port")));
                Log.Logger.Information("Job-Query has been evaluated returning true");

                // Select The data specified and send it to parent
                string payload = JsonSerializer.Serialize(handler.GetSelectResults(selectStatement));  

                var msg = new Message(payload, MessageType.RESPONSEAPI,IpUtils.GetLocalIp(),-1);   // TODO: add local ip and port           
                var sender = new NetworkSender(endPoint, JsonSerializer.Serialize(msg));
                var senderThread = new Thread(() => sender.SendMessage());
                senderThread.Start();
            }
        }
    }
}
