using Hangfire;
using HangFireWorker.JobTypes;
using HangFireWorker.Logs;
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
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public void SetRecurringJobs()
        {
            RecurringJob.RemoveIfExists("myrecurringjob");
            RecurringJob.AddOrUpdate("myrecurringjob",() => Console.WriteLine("Recurring!"),Cron.Daily);


            //RecurringJob.RemoveIfExists(nameof(CreateDailyExchange));
            //RecurringJob.AddOrUpdate<CreateDailyExchange>(nameof(CreateDailyExchange),job => job.CreateExchange(),
            //    Cron.MinuteInterval(1)); //"59 23 * * *",TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate("",() => HataDenemeYap(),Cron.MinuteInterval(1));
        }
        public void HataDenemeYap()
        {
            throw new Exception();
        }

        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public async Task SetRecurringJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
