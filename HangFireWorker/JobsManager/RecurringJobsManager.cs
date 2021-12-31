using Hangfire;
using HangFireWorker.JobTypes;
using SampleMethods;
using SerilogSeqLogger.LogCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{
    public class RecurringJobsManager :IrecurringJobs
    {
        [Obsolete]
        public  void SetRecurringJobs()
        {
            
            //RecurringJob.RemoveIfExists("myrecurringjob");
            //RecurringJob.AddOrUpdate("myrecurringjob",() => Console.WriteLine("Recurring!"),Cron.Daily);
            //RecurringJob.AddOrUpdate<CreateDailyExchange>(nameof(CreateDailyExchange),job => job.CreateExchange(),Cron.MinuteInterval(1)); //"59 23 * * *",TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("Debene Methodlari",() => DividedZeroSample.Execute(),Cron.MinuteInterval(1));
        }
        [Obsolete]
        public async Task SetRecurringJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
