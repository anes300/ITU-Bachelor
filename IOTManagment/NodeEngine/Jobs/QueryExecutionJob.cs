﻿using Model.Queries;
using Model.Queries.Statements;
using NodeEngine.Services;
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
            SensorManager sensorManager = new SensorManager();
            QueryHandler handler = new QueryHandler(sensorManager);

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            SelectStatement? selectStatement = JsonSerializer.Deserialize<SelectStatement>(dataMap.GetString("Select"));
            WhereStatement? whereStatement = JsonSerializer.Deserialize<WhereStatement>(dataMap.GetString("Where"));
            
            if(whereStatement != null && handler.CheckWhereStatement(whereStatement))
            {
                Log.Logger.Information("Job-Query has been evaluated returning true");
                // Select The data specified and send it to parent
            }
        }
    }
}
