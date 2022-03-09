using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Impl;
using Quartz;
using Model.Queries;
using NodeEngine.Jobs;
using System.Text.Json;

namespace NodeEngine.Services
{
    public class QueryScheduler : IDisposable
    {

        IScheduler scheduler;
        public QueryScheduler()
        {
            // using defaults
            StdSchedulerFactory factory = new StdSchedulerFactory();

            scheduler = factory.GetScheduler().Result;

            scheduler.Start().Wait();
            
        }

        public async Task AddQueryJobAsync(Query query)
        {
            int interval = query.IntervalStatement.Interval;

            string selectStatement = JsonSerializer.Serialize(query.SelectStatement);

            string whereStatement = JsonSerializer.Serialize(query.WhereStatement);

            // create the job and inputs the query data into the jobs datamap as String
            IJobDetail job = JobBuilder.Create<QueryExecutionJob>()
                .WithIdentity($"Job-{query.Id}", "Queries")
                .UsingJobData("Select", selectStatement)
                .UsingJobData("Where", whereStatement)
                .Build();
            
            // Create the jobs trigger with the interval specified in the given query
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"Trigger-{query.Id}", "QETriggers")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMilliseconds(interval))
                    .RepeatForever())
                .Build();
            
            // Schedule the job using The created trigger
            await scheduler.ScheduleJob(job, trigger);

        }

        // Remove a job from the schedule
        public async Task RemoveQueryjobAsync(Guid queryId)
        {
            await scheduler.DeleteJob(new JobKey($"Job-{queryId}", "Queries"));
        }

        public void Dispose()
        {
            scheduler.Shutdown();
                
        }

        
    }
}
