using Model.Queries;
using Model.Queries.Statements;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NodeEngine.Jobs
{
    public class QueryExecutionJob : IJob
    {
        
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            SelectStatement? selectStatement = JsonSerializer.Deserialize<SelectStatement>(dataMap.GetString("Select"));
            WhereStatement? whereStatement = JsonSerializer.Deserialize<WhereStatement>(dataMap.GetString("Where"));

            Log.Logger.Information("Log Something");
            await Console.Error.WriteLineAsync("Select variable: " + selectStatement.Variables[0].Variable);
        }
    }
}
